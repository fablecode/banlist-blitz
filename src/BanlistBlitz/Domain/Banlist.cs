namespace BanlistBlitz.Domain;

public  abstract class Banlist
{
    protected Banlist(string name, Format format, DateTime releaseDate)
    {
        Name = name;
        Format = format;
        ReleaseDate = releaseDate;
    }

    public string Name { get; }
    public Format Format { get; }
    public DateTime ReleaseDate { get; }
}
