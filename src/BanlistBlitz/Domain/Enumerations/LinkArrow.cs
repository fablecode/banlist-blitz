using Ardalis.SmartEnum;

namespace BanlistBlitz.Domain.Enumerations;

public class LinkArrow : SmartEnum<LinkArrow>
{
    public static readonly LinkArrow TopLeft = new ("Top-Left", 1);
    public static readonly LinkArrow Top = new (nameof(Top), 2);
    public static readonly LinkArrow TopRight = new ("Top-Right", 3);
    public static readonly LinkArrow Right = new (nameof(Right), 4);
    public static readonly LinkArrow BottomRight = new ("Bottom-Right", 5);
    public static readonly LinkArrow Bottom = new (nameof(Bottom), 6);
    public static readonly LinkArrow BottomLeft = new ("Bottom-Left", 7);
    public static readonly LinkArrow Left = new (nameof(Left), 8);

    public LinkArrow(string name, int value) : base(name, value)
    {
    }
}