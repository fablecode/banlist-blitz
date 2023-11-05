using System.Data;
using BanlistBlitz.Domain;
using BanlistBlitz.Domain.Enumerations;
using BanlistBlitz.Exceptions;
using HtmlAgilityPack;

namespace BanlistBlitz.Helpers;

public static class CardHelper
{
    public static Card FromCardType(string name, string cardType)
    {
        var asTitleCase =
            Thread.CurrentThread.CurrentCulture.TextInfo.
                ToTitleCase(name.ToLower());

        if (cardType.Contains("Monster", StringComparison.OrdinalIgnoreCase))
        {
            return new MonsterCard(asTitleCase);
        }

        if (cardType.Equals("Spell", StringComparison.OrdinalIgnoreCase))
        {
            return new SpellCard(SpellSubCategory.Normal)
            {
                Name = asTitleCase
            };
        }

        return new TrapCard(TrapSubCategory.Normal)
        {
            Name = asTitleCase
        };
    }

    public static BanlistCard FromDataRow(DataRow cardRow)
    {
        if (cardRow == null) 
            throw new ArgumentNullException(nameof(cardRow));

        var cardName = cardRow.Field<string>("Card Name") ?? throw new CardNameException(cardRow);
        var cardType = cardRow.Field<string>("Card Type") ?? throw new CardTypeException(cardRow);
        var remarks = cardRow.Field<string>("Remarks");

        var cardNameTitleCased =
            HtmlEntity.DeEntitize(Thread.CurrentThread.CurrentCulture.TextInfo.
                ToTitleCase(cardName.ToLower().RemoveExtraSpaceBetweenTwoWords()));

        if (cardType.Contains("Monster", StringComparison.OrdinalIgnoreCase))
        {
            return new MonsterCard(cardNameTitleCased)
            {
                Name = cardNameTitleCased,
                Remarks = remarks
            };
        }

        if (cardType.Equals("Spell", StringComparison.OrdinalIgnoreCase))
        {
            return new SpellCard(SpellSubCategory.Normal)
            {
                Name = cardNameTitleCased,
                Remarks = remarks
            };
        }

        return new TrapCard(TrapSubCategory.Normal)
        {
            Name = cardNameTitleCased,
            Remarks = remarks
        };
    }

    public static BanlistCard FromOcgDataRow(DataRow cardRow)
    {
        if (cardRow == null)
            throw new ArgumentNullException(nameof(cardRow));

        var cardName = cardRow.Field<string>("English Name") ?? throw new CardNameException(cardRow);
        var updates = cardRow.Field<string>("Updates");
    }
}