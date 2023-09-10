using System.Data;
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

        var latestDocument = await web.LoadFromWebAsync(banlistLink.AbsoluteUri);

        var query = from table in latestDocument.DocumentNode.SelectNodes("//table")
            where table.Descendants("tr").Count() > 1 //make sure there are rows other than header row
            from row in table.SelectNodes(".//tr[position()>1]") //skip the header row
            from cell in row.SelectNodes("./td")
            from header in table.SelectNodes(".//tr[1]/th") //select the header row cells which is the first tr
            select new
            {
                Table = table.Id,
                Row = row.InnerText,
                Header = header.InnerText,
                CellText = cell.InnerText
            };

        var datatable = new DataTable();

        foreach (var r in query)
        {
            var dr = datatable.NewRow();
            dr[r.Header] = r.CellText;
        }

        return new Banlist();
    }

    public bool Handles(Format format)
    {
        return format == Format.Tcg;
    }
}