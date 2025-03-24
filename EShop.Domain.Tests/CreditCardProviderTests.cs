using EShop.Application;
using EShop.Domain.Enums;
using EShop.Domain.Exceptions;

namespace EShop.Domain.Tests;

public class CreditCardProviderTests
{
    [Fact]
    public void ValidateCard_ShouldThrowCardNumberTooShortException()
    {
        var creditCardService = new CreditCardService();
        string cardNumber = "123456789015";
        Assert.Throws<CardNumberTooShortException>(() => creditCardService.ValidateCard(cardNumber));
    }

    [Fact]
    public void ValidateCard_ShouldThrowCardNumberTooLongException()
    {
        var creditCardService = new CreditCardService();
        string cardNumber = "12345678901234567894";
        Assert.Throws<CardNumberTooLongException>(() =>  creditCardService.ValidateCard(cardNumber));
    }

    [Fact]
    public void ValidateCard_ShouldThrowCardNumberInvalidException()
    {
        var creditCardService = new CreditCardService();
        string cardNumber = "sdfhksdfbsdfsdfsks";
        Assert.Throws<CardNumberInvalidException>(() => creditCardService.ValidateCard(cardNumber));
    }

    [Fact]
    public void GetCardType_ShouldReturnVisa()
    {
        var creditCardService = new CreditCardService();
        string cardNumber = "4111111111111111";
        Assert.Equal(CreditCardProvider.Visa, creditCardService.GetCardType(cardNumber));
    }
}