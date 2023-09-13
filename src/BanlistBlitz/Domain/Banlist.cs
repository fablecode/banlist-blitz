namespace BanlistBlitz.Domain;

public  abstract class Banlist
{
    public string Name { get; set; } = null!;
    public virtual Format Format { get; set; } = null!;
    public DateTime ReleaseDate { get; set; }
}
