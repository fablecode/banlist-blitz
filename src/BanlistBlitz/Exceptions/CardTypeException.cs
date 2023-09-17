using System.Data;

namespace BanlistBlitz.Exceptions;

public class CardTypeException : Exception
{
    public DataRow Card { get; }

    public CardTypeException(DataRow card)
    {
        Card = card;
    }
}