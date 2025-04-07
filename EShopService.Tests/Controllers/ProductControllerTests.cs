using EShop.Domain;
using Moq;
using EShopService.Controllers;

namespace EShopService.Tests.Controllers;

public class ProductControllerTests
{

    private readonly Mock<ProductService> _mockService;
    private readonly ProductController _controller;

    public ProductControllerTests()
    {
        _mockService = new Mock<ProductService>(MockBehavior.Strict, null!);
        _controller = new ProductController(_mockService.Object);
    }
}