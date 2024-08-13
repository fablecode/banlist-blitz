using BanlistBlitz.Domain;
using BanlistBlitz.Processors;

namespace BanlistBlitz;

public class BanlistBlitz : IBanlistBlitz
{
    private readonly IEnumerable<IFormatProcessor> _formatProcessors;

    public BanlistBlitz()
        : this([new TcgFormatProcessor(), new OcgFormatProcessor()])
    {
        
    }

    public BanlistBlitz(IEnumerable<IFormatProcessor> formatProcessors)
    {
        _formatProcessors = formatProcessors;
    }
    public Task<Banlist> LatestBanlist(Format format)
    {
        var handler = _formatProcessors.Single(h => h.Handles(format));

        return handler.LatestAsync();
    }
}