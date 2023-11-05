namespace BanlistBlitz.Domain;

public sealed class OcgBanlist : Banlist
{
    public required CardRestriction AdvancedFormat { get; set; }
}