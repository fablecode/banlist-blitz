namespace BanlistBlitz.Domain;

public class Banlist
{
    public string Name { get; set; } = null!;
    public virtual IFormat Format { get; set; } = null!;
    public DateTime ReleaseDate { get; set; }
    public ICollection<Card>? Banned { get; set; }
    public ICollection<Card>? Limited { get; set; }
    public ICollection<Card>? SemiLimited { get; set; }
    public ICollection<Card>? Unlimited { get; set; }
}