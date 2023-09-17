using BanlistBlitz.Domain.Enumerations;

namespace BanlistBlitz.Domain;

public sealed class TrapCard : BanlistCard
{
    public TrapCard(SubCategory subCategory)
    {
        SubCategory = subCategory;
    }

    public override Category CardType => Category.Trap;
    public SubCategory SubCategory { get; }
}