namespace BanlistBlitz.Domain;

public sealed class CardRestriction
{
    public ICollection<BanlistCard>? Banned { get; set; }
    public ICollection<BanlistCard>? Limited { get; set; }
    public ICollection<BanlistCard>? SemiLimited { get; set; }
    public ICollection<BanlistCard>? Unlimited { get; set; }
}