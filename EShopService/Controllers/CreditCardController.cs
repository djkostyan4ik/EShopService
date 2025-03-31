using EShop.Application;
using EShop.Domain.Enums;
using EShop.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace EShopService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CreditCardController : ControllerBase
{
    [HttpGet("{cardNumber}")]
    public IActionResult ValidateCard(string cardNumber)
    {
        var creditCardService = new CreditCardService();
        try
        {
            creditCardService.ValidateCard(cardNumber);
        }
        catch (CardNumberTooLongException)
        {
            return StatusCode(414);
        }
        catch (CardNumberTooShortException)
        {
            return StatusCode(400, new { Error = "Card number is too short. Minimum length is 13 digits." });
        }
        catch (CardNumberInvalidException)
        {
            return StatusCode(400, new { Error = "Card number is invalid. It does not pass the Luhn algorithm check."});
        }

        CreditCardProvider? provider = creditCardService.GetCardType(cardNumber);
        if(provider == null)
        {
            return StatusCode(406);
        }
        return Ok(new { Status = "Valid", Provider = provider.Value.ToString() });

    }
}