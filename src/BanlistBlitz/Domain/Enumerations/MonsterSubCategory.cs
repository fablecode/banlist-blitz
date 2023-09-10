namespace BanlistBlitz.Domain.Enumerations;

public class MonsterSubCategory : SubCategory
{
    public static readonly MonsterSubCategory Normal = new(nameof(Normal), 1);
    public static readonly MonsterSubCategory Effect = new(nameof(Effect), 2);
    public static readonly MonsterSubCategory Fusion = new(nameof(Fusion), 3);
    public static readonly MonsterSubCategory Ritual = new(nameof(Ritual), 4);
    public static readonly MonsterSubCategory Synchro = new(nameof(Synchro), 5);
    public static readonly MonsterSubCategory Xyz = new(nameof(Xyz), 6);
    public static readonly MonsterSubCategory Pendulum = new(nameof(Pendulum), 7);
    public static readonly MonsterSubCategory Tuner = new(nameof(Tuner), 8);
    public static readonly MonsterSubCategory Gemini = new(nameof(Gemini), 9);
    public static readonly MonsterSubCategory Union = new(nameof(Union), 10);
    public static readonly MonsterSubCategory Spirit = new(nameof(Spirit), 11);
    public static readonly MonsterSubCategory Flip = new(nameof(Flip), 12);
    public static readonly MonsterSubCategory Toon = new(nameof(Toon), 13);
    public static readonly MonsterSubCategory Link = new(nameof(Link), 14);

    private protected MonsterSubCategory(string name, int id) : base(name, id)
    {
    }

    public override Category Category => Category.Monster;
}