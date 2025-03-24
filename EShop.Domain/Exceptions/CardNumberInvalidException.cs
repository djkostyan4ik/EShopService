namespace EShop.Domain.Exceptions;

public class CardNumberInvalidException : Exception
{

    public CardNumberInvalidException() : base("Card Number is invalid") { }

}