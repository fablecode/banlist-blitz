using Ardalis.SmartEnum;

namespace BanlistBlitz.Domain.Enumerations;

public class Category : SmartEnum<Category>
{
    public static readonly Category Monster = new(nameof(Monster), 1);
    public static readonly Category Spell = new(nameof(Spell), 2 );
    public static readonly Category Trap = new(nameof(Trap), 3);

    private Category(string name, int id) : base(name, id)
    {
    }
}