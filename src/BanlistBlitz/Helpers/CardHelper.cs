using BanlistBlitz.Domain;
using HtmlAgilityPack;
using System.Data;

namespace BanlistBlitz.Helpers;

public static class CardHelper
{
    public static TcgBanlistCard FromDataRow(DataRow cardRow)
    {
        if (cardRow == null) 
            throw new ArgumentNullException(nameof(cardRow));

        var cardName = cardRow.Field<string>("CardName") ?? string.Empty;
        var cardType = cardRow.Field<string>("CardType") ?? string.Empty;
        var advancedFormat = cardRow.Field<string>("AdvancedFormat") ?? string.Empty;
        var traditionalFormat = cardRow.Field<string>("TraditionalFormat") ?? string.Empty;
        var remarks = cardRow.Field<string>("Remarks");

        var cardNameTitleCased =
            HtmlEntity.DeEntitize(Thread.CurrentThread.CurrentCulture.TextInfo.
                ToTitleCase(cardName.ToLower().RemoveExtraSpaceBetweenTwoWords()));

        return new TcgBanlistCard(cardType.Split('/'), cardNameTitleCased, advancedFormat, traditionalFormat, remarks);

    }

    public static OcgBanlistCard FromOcgDataRow(DataRow cardRow)
    {
        if (cardRow == null)
            throw new ArgumentNullException(nameof(cardRow));

        var japaneseCardName = cardRow.Field<string>("Japanese Name") ?? string.Empty;
        var englishCardName = cardRow.Field<string>("English Name") ?? string.Empty;
        var updates = cardRow.Field<string>("Updates")?.Trim('\r', '\n');

        var englishCardNameTitleCased =
            HtmlEntity.DeEntitize(Thread.CurrentThread.CurrentCulture.TextInfo.
                ToTitleCase(englishCardName.ToLower().RemoveExtraSpaceBetweenTwoWords()));

        return new OcgBanlistCard(japaneseCardName, englishCardNameTitleCased, updates);
    }
}