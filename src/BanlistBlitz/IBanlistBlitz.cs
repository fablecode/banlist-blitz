using BanlistBlitz.Domain;

namespace BanlistBlitz
{
    public interface IBanlistBlitz
    {
        Task<Banlist> LoadBanlist(Format format);
    }
}