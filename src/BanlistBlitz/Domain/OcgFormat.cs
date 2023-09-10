namespace BanlistBlitz.Domain;

public sealed class OcgFormat : IFormat
{
    public string Name => "Ocg";
    public Uri Url => new("https://www.yugioh-card.com/hk/event/rules_guides/forbidden_cardlist.php");
}