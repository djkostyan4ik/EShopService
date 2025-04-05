using EShop.Domain.Enums;

namespace EShop.Application;

public interface ICreditCardService
{
    void ValidateCard(string cardNumber);

    CreditCardProvider? GetCardProvider(string cardNumber);
}
