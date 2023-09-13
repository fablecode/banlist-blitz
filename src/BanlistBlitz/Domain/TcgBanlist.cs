namespace BanlistBlitz.Domain;

public sealed class TcgBanlist : Banlist
{
    public required CardRestriction TraditionalFormat { get; set; }
    public required CardRestriction AdvancedFormat { get; set; }
}