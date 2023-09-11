using Ardalis.SmartEnum;

namespace BanlistBlitz.Domain;

public class Format : SmartEnum<Format>
{
    public static readonly Format Tcg = new Format(nameof(Tcg), 1);
    public static readonly Format Ocg = new Format(nameof(Ocg), 2);

    public Format(string name, int value) : base(name, value)
    {
    }
}