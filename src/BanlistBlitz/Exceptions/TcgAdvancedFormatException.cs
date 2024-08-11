using System.Data;

namespace BanlistBlitz.Exceptions;

public class TcgAdvancedFormatException : Exception
{
    public DataRow Card { get; }

    public TcgAdvancedFormatException(DataRow card)
    {
        Card = card;
    }
}