namespace EShop.Domain.Exceptions;

public class CardNumberTooShortException : Exception
{

    public CardNumberTooShortException() : base("Card Number is too short") { }

}
