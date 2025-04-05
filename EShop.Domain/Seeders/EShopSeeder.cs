using EShop.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace EShop.Domain.Seeders;

internal static class EShopSeeder
{
    public static List<Product> GetInitialProducts()
    {
        return new List<Product>
        {
            new Product 
            { 
                Id = 1,
                Name = "Coffee",
                Ean = "123321423423",
                Price = 25.99m,
                Stock = 100,
                Sku = "SKU01", 
            },
            new Product 
            { 
                Id = 2,
                Name = "Green Tea with lemon",
                Ean = "1234567890123",
                Price = 14.99m,
                Stock = 150,
                Sku = "SKU02",
            },
            new Product 
            {
                Id = 3,
                Name = "Cocoa",
                Ean = "3445679810234",
                Price = 16.99m,
                Stock = 100,
                Sku = "SKU03",
            }
        };
    }
}
