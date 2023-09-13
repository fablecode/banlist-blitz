namespace BanlistBlitz.Domain;

public sealed class CardRestriction
{
    public ICollection<Card>? Banned { get; set; }
    public ICollection<Card>? Limited { get; set; }
    public ICollection<Card>? SemiLimited { get; set; }
    public ICollection<Card>? Unlimited { get; set; }
}