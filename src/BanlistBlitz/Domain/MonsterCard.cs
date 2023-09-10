using BanlistBlitz.Domain.Enumerations;
using Attribute = BanlistBlitz.Domain.Enumerations.Attribute;
using Type = BanlistBlitz.Domain.Enumerations.Type;

namespace BanlistBlitz.Domain;

public sealed class MonsterCard : Card
{
    public MonsterCard(Attribute attribute, List<SubCategory> subCategories, List<Type> types)
    {
        Attribute = attribute;
        SubCategories = subCategories;
        Types = types;
    }

    public override Category CardType => Category.Monster;
    public Attribute Attribute { get; }
    public int? CardLevel { get; set; }
    public int? CardRank { get; set; }
    public int? Atk { get; set; }
    public int? Def { get; set; }
    public List<SubCategory> SubCategories { get; set; }
    public List<Type> Types { get; set; }
    public List<LinkArrow>? LinkArrows { get; set; }
}