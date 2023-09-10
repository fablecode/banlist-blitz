using Ardalis.SmartEnum;

namespace BanlistBlitz.Domain;

public sealed class TcgFormat : IFormat
{
    public string Name => "Tcg";
    public Uri Url => new("https://www.yugioh-card.com/en/limited/");
}

public class Format : SmartEnum<Format>
{
    public static readonly Format Tcg = new Format(nameof(Tcg), 1);
    public static readonly Format Ocg = new Format(nameof(Ocg), 2);

    public Format(string name, int value) : base(name, value)
    {
    }
}