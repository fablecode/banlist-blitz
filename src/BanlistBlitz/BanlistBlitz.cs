using System.Data;
using System.Runtime.InteropServices.JavaScript;
using BanlistBlitz.Domain;
using HtmlAgilityPack;

namespace BanlistBlitz;

public class BanlistBlitz : IBanlistBlitz
{
    private readonly IEnumerable<IFormatProcessor> _formatProcessors;

    public BanlistBlitz(IEnumerable<IFormatProcessor> formatProcessors)
    {
        _formatProcessors = formatProcessors;
    }
    public Task<Banlist> LoadBanlist(Format format)
    {
        var handler = _formatProcessors.Single(h => h.Handles(format));

        return handler.LatestAsync();
    }
}

public interface IFormatProcessor
{
    Task<Banlist> LatestAsync();
    bool Handles(Format format);
}

public class TcgFormatProcessor : IFormatProcessor
{
    private string url => new("https://www.yugioh-card.com/en/limited/");

    public async Task<Banlist> LatestAsync()
    {
        var web = new HtmlWeb();
        var document = await web.LoadFromWebAsync(url);

        var linkHtmlNode = document.DocumentNode.SelectSingleNode("//a[contains(@class, 'wp-block-button__link has-text-color has-background')]");
        var banlistLink = new Uri(new Uri(new Uri(url).GetLeftPart(UriPartial.Path)), linkHtmlNode.Attributes["href"].Value);

        var latestBanlistDocument = await web.LoadFromWebAsync(banlistLink.AbsoluteUri);

        //var query = from table in latestDocument.DocumentNode.SelectNodes("//table[contains(@class, 'cardlist')]")
        //    where table.Descendants("tr").Count() > 1 //make sure there are rows other than header row
        //    from row in table.SelectNodes(".//tr[position()>1]") //skip the header row
        //    from cell in row.SelectNodes("./td")
        //    from header in table.SelectNodes(".//tr[1]/td") //select the header row cells which is the first tr
        //    select new
        //    {
        //        Table = table.Id,
        //        Row = row.SelectNodes("td").Select(td => td.InnerText).ToArray(),
        //        Header = header.InnerText,
        //        CellText = cell.InnerText
        //    };

        var query = from table in latestBanlistDocument.DocumentNode.SelectNodes("//table[contains(@class, 'cardlist')]")
            where table.Descendants("tr").Any() //make sure there are rows other than header row
            from row in table.SelectNodes(".//tr[position()>1]") //skip the header row
            select row.SelectNodes("td").Select(td => td.InnerText).ToArray<object>();


        var datatable = new DataTable("TcgBanlist");
        datatable.Columns.Add("Card Type", typeof(string));
        datatable.Columns.Add("Card Name", typeof(string));
        datatable.Columns.Add("Advanced Format", typeof(string));
        datatable.Columns.Add("Traditional Format", typeof(string));
        datatable.Columns.Add("Remarks", typeof(string));

        foreach (var rowData in query)
        {
            datatable.Rows.Add(rowData);
        }

        var banlist = new TcgBanlist
        {
            TraditionalFormat = null,
            AdvancedFormat = null
        };

        banlist.Format = Format.Tcg;
        banlist.Name = HtmlEntity.DeEntitize(latestBanlistDocument
            .DocumentNode
            .SelectSingleNode("//header/h1[contains(@class, 'entry-title')]").InnerText);
        banlist.ReleaseDate = DateTime.Parse(latestBanlistDocument
            .DocumentNode
            .SelectSingleNode("//div[contains(@class, 'entry-content')]/h3")
            .InnerText
            .TrimStart("Effective from ".ToCharArray()));

        banlist.AdvancedFormat = new CardRestriction
        {
            Banned = (from card in datatable.AsEnumerable()
                     where card.Field<string>("Advanced Format") == "Forbidden"
                         select new Card
                         {
                             Name = card.Field<string>("Card Name")
                         })
                .ToList()
        }

        //foreach (var table in latestDocument.DocumentNode.SelectNodes("//table[contains(@class, 'cardlist')]").Where(t => t.Descendants("tr").Any()))
        //{
        //    foreach (var row in table.SelectNodes(".//tr[position()>1]"))
        //    {
        //        datatable.Rows.Add(row.SelectNodes("td").Select(td => td.InnerText).ToArray<object>());
        //    }
        //}




        //foreach (var r in query)
        //{
        //    var dr = datatable.NewRow();
        //    dr[r.Header] = r.CellText;
        //}

        return banlist
            ;
    }

    public bool Handles(Format format)
    {
        return format == Format.Tcg;
    }
}