using BanlistBlitz.Domain;
using BanlistBlitz.Exceptions;
using BanlistBlitz.Helpers;
using HtmlAgilityPack;
using System.Data;

namespace BanlistBlitz.Processors;

public sealed class OcgFormatProcessor : IFormatProcessor
{
    private const string Japanese = "Japanese Name";
    private const string English = "English Name";
    private const string Updates = "Updates";
    private static string BanlistUrl => new("https://www.yugioh-card.com/hk/event/rules_guides/forbidden_cardlist.php");
    public async Task<Banlist> LatestAsync()
    {
        var web = new HtmlWeb();
        var document = await web.LoadFromWebAsync(BanlistUrl);
        
        var linkHtmlNode = document.DocumentNode.SelectSingleNode("//a[contains(@class, 'big_btn')]");
        var banlistLink = new Uri(new Uri(new Uri(BanlistUrl).GetLeftPart(UriPartial.Path)), linkHtmlNode.Attributes["href"].Value);
        var banlistLinkText = linkHtmlNode.InnerText;
        
        var latestBanlistDocument = await web.LoadFromWebAsync(banlistLink.AbsoluteUri);

        var forbiddenListQuery = BanlistListQuery(latestBanlistDocument, "Forbidden Cards:");

        var limitedListQuery = BanlistListQuery(latestBanlistDocument, "Limited Cards:");

        var semiLimitedListQuery = BanlistListQuery(latestBanlistDocument, "Semi-Limited Cards:");

        var unlimitedListQuery = BanlistListQuery(latestBanlistDocument, "No Longer On List:");

        var forbiddenCards = new DataTable("ForbiddenCards");
        forbiddenCards.Columns.AddRange(BanlistListColumns());

        var limitedCards = new DataTable("LimitedCards");
        limitedCards.Columns.AddRange(BanlistListColumns());

        var semiLimitedCards = new DataTable("SemiLimitedCards");
        semiLimitedCards.Columns.AddRange(BanlistListColumns());

        var unlimitedCards = new DataTable("UnlimitedCards");
        unlimitedCards.Columns.AddRange(BanlistListColumns());


        foreach (var rowData in forbiddenListQuery)
        {
            forbiddenCards.Rows.Add(rowData);
        }
        foreach (var rowData in limitedListQuery)
        {
            limitedCards.Rows.Add(rowData);
        }
        foreach (var rowData in semiLimitedListQuery)
        {
            semiLimitedCards.Rows.Add(rowData);
        }
        foreach (var rowData in unlimitedListQuery)
        {
            unlimitedCards.Rows.Add(rowData);
        }

        var banlist = new OcgBanlist
        (
            HtmlEntity.DeEntitize(latestBanlistDocument
                .DocumentNode
                .SelectSingleNode("//section/h1").InnerText.Trim()),
            Format.Ocg,
            DateTime.Parse(banlistLinkText.Trim().TrimStart("Latest Version: ".ToCharArray()))
        );

        banlist.Banned =
            (
                from card in forbiddenCards.AsEnumerable()
                select FromOcgDataRow(card)
            )
            .ToList();

        banlist.Limited =
            (
                from card in limitedCards.AsEnumerable()
                select FromOcgDataRow(card)
            )
            .ToList();

        banlist.SemiLimited =
            (
                from card in semiLimitedCards.AsEnumerable()
                select FromOcgDataRow(card)
            )
            .ToList();

        banlist.Unlimited =
            (
                from card in unlimitedCards.AsEnumerable()
                select FromOcgDataRow(card)
            )
            .ToList();
        

        return banlist;

        DataColumn[] BanlistListColumns()
        {
            return new[]
            {
                new DataColumn(Japanese, typeof(string)),
                new DataColumn(English, typeof(string)),
                new DataColumn(Updates, typeof(string))
            };
        }

        IEnumerable<object[]> BanlistListQuery(HtmlDocument htmlDocument, string category)
        {
            return from row in htmlDocument.DocumentNode
                    .SelectSingleNode($"//h2[contains(text(), '{category}')]//following-sibling::table[contains(@class, 'limit_list_style')]")
                    .SelectNodes(".//tr[contains(@class, 'news')]")
                select row.SelectNodes("td").Select(td => td.InnerText).ToArray<object>();
        }
    }

    public bool Handles(Format format)
    {
        return format == Format.Ocg;
    }

    #region Helpers
    public static OcgBanlistCard FromOcgDataRow(DataRow cardRow)
    {
        if (cardRow == null)
            throw new ArgumentNullException(nameof(cardRow));

        var japaneseCardName = cardRow.Field<string>(Japanese) ?? throw new CardNameException(cardRow);
        var englishCardName = cardRow.Field<string>(English) ?? throw new CardNameException(cardRow);
        var updates = cardRow.Field<string>(Updates)?.Trim('\r', '\n');

        var englishCardNameTitleCased =
            HtmlEntity.DeEntitize(Thread.CurrentThread.CurrentCulture.TextInfo.
                ToTitleCase(englishCardName.ToLower().RemoveExtraSpaceBetweenTwoWords()));

        return new OcgBanlistCard(japaneseCardName, englishCardNameTitleCased, updates);
    }

    #endregion
}