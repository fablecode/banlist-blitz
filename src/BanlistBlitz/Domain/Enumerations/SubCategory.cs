using Ardalis.SmartEnum;

namespace BanlistBlitz.Domain.Enumerations;

public abstract class SubCategory : SmartEnum<SubCategory>
{
    public abstract Category Category { get; }

    private protected SubCategory(string name, int id) : base(name, id)
    {
    }
}