namespace BanlistBlitz.Domain.Enumerations;

public class SpellSubCategory : SubCategory
{
    public static readonly SpellSubCategory Normal = new(nameof(Normal), 1);
    public static readonly SpellSubCategory QuickPlay = new ("Quick-Play", 2);
    public static readonly SpellSubCategory Continuous = new(nameof(Continuous), 3);
    public static readonly SpellSubCategory Ritual = new(nameof(Ritual), 4);
    public static readonly SpellSubCategory Equip = new(nameof(Equip), 5);
    public static readonly SpellSubCategory Field = new(nameof(Field), 6);

    private protected SpellSubCategory(string name, int id) : base(name, id)
    {
    }

    public override Category Category => Category.Spell;
}