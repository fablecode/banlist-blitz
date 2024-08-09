namespace BanlistBlitz.Domain;

public record TcgBanlistCard(string[] CardTypes, string CardName, string AdvancedFormat, string TraditionalFormat, string? Remarks);