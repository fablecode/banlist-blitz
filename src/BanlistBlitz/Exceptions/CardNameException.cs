using System.Data;

namespace BanlistBlitz.Exceptions;

public class CardNameException : Exception
{
    public DataRow Card { get; }

    public CardNameException(DataRow card)
    {
        Card = card;
    }
}