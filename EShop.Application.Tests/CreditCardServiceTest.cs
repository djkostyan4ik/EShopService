using EShop.Domain.Enums;
using EShop.Domain.Exceptions;

namespace EShop.Application.Tests;

public class CreditCardServiceTest
{
    // should accept a text type of 13-19 characters including dashes/spaces between and consistent with the Luhn algorithm

    [Theory]
    [InlineData("3497 7965 8312 797")]
    [InlineData("345-470-784-783-010")] // valid, dashes; 15 characters 
    [InlineData("4547078478308668673")] // 19 characters
    [InlineData("4532289052803")] // 13 characters
    [InlineData("4-5-3---2289052803")] // 18 characters, many dashes
    [InlineData("453228905 -  -2803")] // 13 characters, dashes i spaces
    [InlineData("0000000000000")] // luhn algorithm wrong second digit
    [InlineData("1020202010101")] // luhn algorithm correct code without alternate
    [InlineData("2040404020202")] // luhn algorithm correct code without alternate
    [InlineData("0001010101010")] // luhn algorithm correct self alternate code

    public void ValidateCard_CorrectLength_ExpectedTrue(string cardNumber)
    {
        var creditCardService = new CreditCardService();

        creditCardService.ValidateCard(cardNumber);

        var exception = Record.Exception(() => creditCardService.ValidateCard(cardNumber));

        Assert.Null(exception);
    }

    [Theory]
    [InlineData("345-470")]
    [InlineData("378523393817")] // 12 characters
    [InlineData("")]
    [InlineData("-")]
    public void ValidateCard_CorrectLenght_ExpectedCardNumberTooShortException(string cardNumber)
    {
        var creditCardService = new CreditCardService();

        Assert.Throws<CardNumberTooShortException>(() => creditCardService.ValidateCard(cardNumber));
    }


    [Theory]
    [InlineData("3497 7965 8312 79729876")] // 20 characters

    public void ValidateCard_CorrectLength_ExpectedCardNumberTooLongException(string cardNumber)
    {
        var creditCardService = new CreditCardService();

        Assert.Throws<CardNumberTooLongException>(() => creditCardService.ValidateCard(cardNumber));
    }

    [InlineData("453_228_905_2803")] // 13 characters, underscores
    [InlineData("4532289052808")] // luhn algorithm incorrect last digit
    [InlineData("4532289052819")] // luhn algorithm incorrect penultimate digit
    [InlineData("3532289052809")] // luhn algorithm incorrect first digit
    [InlineData("4632289052809")] // luhn algorithm incorrect second digit

    [InlineData("alamakotaala")]
    public void ValidateCard_InvalidChecksum_ThrowsCardNumberInvalidException(string cardNumber)
    {
        var creditCardService = new CreditCardService();

        Assert.Throws<CardNumberInvalidException>(() => creditCardService.ValidateCard(cardNumber));
    }


    [Theory]
    [InlineData("4024-0071-6540-1778", CreditCardProvider.Visa)] //numbers started with 4
    [InlineData("5530016454538418", CreditCardProvider.Mastercard)] // 51-55 or 2221-2720
    [InlineData("3497 7965 8312 797", CreditCardProvider.AmericanExpress)] // 34 or 37
    [InlineData("6011 0000 0000 0000", CreditCardProvider.Discover)] // 6011, 65, 644-649
    [InlineData("3589626915830868", CreditCardProvider.JCB)] // 3528-3589
    [InlineData("3050 0000 0000 00", CreditCardProvider.DinersClub)] // 300-305, 36, 38
    [InlineData("5900 0000 0000 0000", CreditCardProvider.Maestro)] // 50, 56-69

    public void GetCardType_CorrectBankName_ExpectedTrue(string cardNumber, CreditCardProvider? expected)
    {
        var creditCardService = new CreditCardService();
        CreditCardProvider? result = creditCardService.GetCardType(cardNumber);

        Assert.Equal(expected, result);

    }


    [Theory]
    [InlineData("0000 0000 0000 0000", "Nieznany wydawca")] // not known number
    public void GetCardType_ShouldReturnCorrectCardType_unknow(string cardNumber, string expected)
    {
        var creditCardService = new CreditCardService();
        CreditCardProvider? result = creditCardService.GetCardType(cardNumber);
        Assert.Null(result);
    }

}
