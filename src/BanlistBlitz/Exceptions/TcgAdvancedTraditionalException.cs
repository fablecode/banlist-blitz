using System.Data;

namespace BanlistBlitz.Exceptions;

public class TcgAdvancedTraditionalException : Exception
{
    public DataRow Card { get; }

    public TcgAdvancedTraditionalException(DataRow card)
    {
        Card = card;
    }
}