using BanlistBlitz.Domain.Enumerations;

namespace BanlistBlitz.Domain;

public abstract class Card
{
    public long? CardNumber { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public abstract Category CardType { get; }
}