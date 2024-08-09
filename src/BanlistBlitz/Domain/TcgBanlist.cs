namespace BanlistBlitz.Domain;

public sealed class TcgBanlist : Banlist
{
    public TcgBanlist(string name, Format format, DateTime releaseDate) : base(name, format, releaseDate)
    {
    }

    public ICollection<TcgBanlistCard>? Banned { get; set; }
                       
    public ICollection<TcgBanlistCard>? Limited { get; set; }
                       
    public ICollection<TcgBanlistCard>? SemiLimited { get; set; }
                       
    public ICollection<TcgBanlistCard>? Unlimited { get; set; }

}