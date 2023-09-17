using BanlistBlitz.Domain;

namespace BanlistBlitz.Processors;

public interface IFormatProcessor
{
    Task<Banlist> LatestAsync();
    bool Handles(Format format);
}