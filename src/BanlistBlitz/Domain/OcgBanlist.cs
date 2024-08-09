namespace BanlistBlitz.Domain;

public sealed class OcgBanlist : Banlist
{
    public OcgBanlist(string name, Format format, DateTime releaseDate) : base(name, format, releaseDate)
    {
    }

    public ICollection<OcgBanlistCard>? Banned { get; set; }

    public ICollection<OcgBanlistCard>? Limited { get; set; }

    public ICollection<OcgBanlistCard>? SemiLimited { get; set; }

    public ICollection<OcgBanlistCard>? Unlimited { get; set; }
}