using BanlistBlitz.Domain;

namespace BanlistBlitz
{
    public interface IBanlistBlitz
    {
        Task<Banlist> LatestBanlist(Format format);
    }
}