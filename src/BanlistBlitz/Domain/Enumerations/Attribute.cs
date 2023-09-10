using Ardalis.SmartEnum;

namespace BanlistBlitz.Domain.Enumerations;

public class Attribute : SmartEnum<Attribute>
{
    public static readonly Attribute Dark = new(nameof(Dark), 1);
    public static readonly Attribute Divine = new(nameof(Divine), 2);
    public static readonly Attribute Fire = new(nameof(Fire), 3);
    public static readonly Attribute Light = new(nameof(Light), 4);
    public static readonly Attribute Water = new(nameof(Water), 5);
    public static readonly Attribute Wind = new(nameof(Wind), 6);

    private Attribute(string name, int value) : base(name, value)
    {
    }
}