using BanlistBlitz.Domain.Enumerations;

namespace BanlistBlitz.Domain;

public sealed class SpellCard : Card
{
    public SpellCard(SubCategory subCategory)
    {
        SubCategory = subCategory;
    }

    public override Category CardType => Category.Spell;
    public SubCategory SubCategory { get; }
}