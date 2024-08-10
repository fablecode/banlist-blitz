using System.Data;
using BanlistBlitz.Domain;
using BanlistBlitz.Helpers;
using HtmlAgilityPack;

namespace BanlistBlitz.Processors;

public class TcgFormatProcessor : IFormatProcessor
{
    private static string BanlistUrl => new("https://www.yugioh-card.com/en/limited/");

    public async Task<Banlist> LatestAsync()
    {
        var web = new HtmlWeb();
        var document = await web.LoadFromWebAsync(BanlistUrl);

        var linkHtmlNode = document.DocumentNode.SelectSingleNode("//a[contains(@class, 'wp-block-button__link has-text-color has-background')]");
        var banlistLink = new Uri(new Uri(new Uri(BanlistUrl).GetLeftPart(UriPartial.Path)), linkHtmlNode.Attributes["href"].Value);

        var latestBanlistDocument = await web.LoadFromWebAsync(banlistLink.AbsoluteUri);

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
        (
            HtmlEntity.DeEntitize(latestBanlistDocument
                .DocumentNode
                .SelectSingleNode("//header/h1[contains(@class, 'entry-title')]").InnerText),
            Format.Tcg,
            DateTime.Parse(latestBanlistDocument
                .DocumentNode
                .SelectSingleNode("//div[contains(@class, 'entry-content')]/h3")
                .InnerText
                .TrimStart("Effective from ".ToCharArray()))
        );

        banlist.Banned =
            (
                from card in datatable.AsEnumerable()
                where card.Field<string>("Advanced Format").RemoveExtraSpaceBetweenTwoWords() == "Forbidden"
                select CardHelper.FromDataRow(card)
            )
            .ToList();

        banlist.Limited =
            (
                from card in datatable.AsEnumerable()
                where card.Field<string>("Advanced Format").RemoveExtraSpaceBetweenTwoWords() == "Limited"
                select CardHelper.FromDataRow(card)
            )
            .ToList();

        banlist.SemiLimited =
            (
                from card in datatable.AsEnumerable()
                where card.Field<string>("Advanced Format").RemoveExtraSpaceBetweenTwoWords() == "Semi-Limited"
                select CardHelper.FromDataRow(card)
            )
            .ToList();

        banlist.Unlimited =
            (
                from card in datatable.AsEnumerable()
                where card.Field<string>("Advanced Format").RemoveExtraSpaceBetweenTwoWords() == "No Longer On List"
                select CardHelper.FromDataRow(card)
            )
            .ToList();

        return banlist;
    }

    public bool Handles(Format format)
    {
        return format == Format.Tcg;
    }
}