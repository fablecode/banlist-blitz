namespace BanlistBlitz.Domain.Enumerations;

public class TrapSubCategory : SubCategory
{
    public static readonly TrapSubCategory Normal = new(nameof(Normal), 1);
    public static readonly TrapSubCategory Continuous = new(nameof(Continuous), 2);
    public static readonly TrapSubCategory Counter = new(nameof(Counter), 3);

    private protected TrapSubCategory(string name, int id) : base(name, id)
    {
    }

    public override Category Category => Category.Trap;
}