using System.Data;
using BanlistBlitz.Domain;
using BanlistBlitz.Exceptions;
using BanlistBlitz.Helpers;
using HtmlAgilityPack;

namespace BanlistBlitz.Processors;

public class TcgFormatProcessor : IFormatProcessor
{
    private const string CardType = "Card Type";
    private const string CardName = "Card Name";
    private const string AdvancedFormat = "Advanced Format";
    private const string TraditionalFormat = "Traditional Format";
    private const string Remarks = "Remarks";
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
        datatable.Columns.Add(CardType, typeof(string));
        datatable.Columns.Add(CardName, typeof(string));
        datatable.Columns.Add(AdvancedFormat, typeof(string));
        datatable.Columns.Add(TraditionalFormat, typeof(string));
        datatable.Columns.Add(Remarks, typeof(string));

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
                where string.Equals(card.Field<string>(AdvancedFormat).RemoveExtraSpaceBetweenTwoWords(), "Forbidden", StringComparison.OrdinalIgnoreCase)
                select FromTcgDataRow(card)
            )
            .ToList();

        banlist.Limited =
            (
                from card in datatable.AsEnumerable()
                where string.Equals(card.Field<string>(AdvancedFormat).RemoveExtraSpaceBetweenTwoWords(), "Limited", StringComparison.OrdinalIgnoreCase)
                select FromTcgDataRow(card)
            )
            .ToList();

        banlist.SemiLimited =
            (
                from card in datatable.AsEnumerable()
                where string.Equals(card.Field<string>(AdvancedFormat).RemoveExtraSpaceBetweenTwoWords(), "Semi-Limited", StringComparison.OrdinalIgnoreCase)
                select FromTcgDataRow(card)
            )
            .ToList();

        banlist.Unlimited =
            (
                from card in datatable.AsEnumerable()
                where string.Equals(card.Field<string>(AdvancedFormat).RemoveExtraSpaceBetweenTwoWords(), "No Longer On List", StringComparison.OrdinalIgnoreCase)
                select FromTcgDataRow(card)
            )
            .ToList();

        return banlist;
    }

    public bool Handles(Format format)
    {
        return format == Format.Tcg;
    }

    #region Helpers
    public static TcgBanlistCard FromTcgDataRow(DataRow cardRow)
    {
        if (cardRow == null)
            throw new ArgumentNullException(nameof(cardRow));

        var cardName = cardRow.Field<string>(CardName) ?? throw new CardNameException(cardRow);
        var cardType = cardRow.Field<string>(CardType) ?? throw new CardTypeException(cardRow);
        var advancedFormat = cardRow.Field<string>(AdvancedFormat) ?? throw new TcgAdvancedFormatException(cardRow);
        var traditionalFormat = cardRow.Field<string>(TraditionalFormat) ?? throw new TcgAdvancedTraditionalException(cardRow);
        var remarks = cardRow.Field<string>(Remarks);

        var cardNameTitleCased =
            HtmlEntity.DeEntitize(Thread.CurrentThread.CurrentCulture.TextInfo.
                ToTitleCase(cardName.ToLower().RemoveExtraSpaceBetweenTwoWords()));

        return new TcgBanlistCard(cardType.Split('/'), cardNameTitleCased, advancedFormat, traditionalFormat, remarks);

    } 
    #endregion
}