using Ardalis.SmartEnum;

namespace BanlistBlitz.Domain;

public interface IFormat
{
    string Name { get; }
    Uri Url { get; }
}