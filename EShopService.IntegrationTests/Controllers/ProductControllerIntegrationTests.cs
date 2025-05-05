﻿using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Text;
using EShop.Domain.Repositories;
using System.Net.Http.Json;
using EShop.Domain.Models;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace EShopService.IntegrationTests.Controllers;

public class ProductControllerIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;
    private WebApplicationFactory<Program> _factory;

    public ProductControllerIntegrationTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    // pobranie dotychczasowej konfiguracji bazy danych
                    var dbContextOptions = services
                        .SingleOrDefault(service => service.ServiceType == typeof(DbContextOptions<DbContext>));

                    // usunięcie dotychczasowej konfiguracji bazy danych
                    services.Remove(dbContextOptions);

                    // Stworzenie nowej bazy danych
                    services
                        .AddDbContext<DataContext>(options => options.UseInMemoryDatabase("MyDBForTest"));

                });
            });

        _client = _factory.CreateClient();
    }

    [Fact]
    public async Task Get_ReturnsAllProducts_ExceptedTwoProducts()
    {
        // Arange
        using (var scope = _factory.Services.CreateScope())
        {
            // Pobranie kontekstu bazy danych
            var dbContext = scope.ServiceProvider.GetRequiredService<DataContext>();

            dbContext.Products.RemoveRange(dbContext.Products);

            // Stworzenie obiektu
            dbContext.Products.AddRange(
                    new Product { Name = "Product1" },
                    new Product { Name = "Product2" }
                );
            // Zapisanie obiektu
            await dbContext.SaveChangesAsync();
        }

        // Act
        var response = await _client.GetAsync("/api/product");

        // Assert
        response.EnsureSuccessStatusCode();
        var products = await response.Content.ReadFromJsonAsync<List<Product>>();
        Assert.Equal(2, products?.Count);
    }

    [Fact]
    public async Task Post_AddThousandsProductsAsync_ExceptedThousandsProducts()
    {
        // Arrange
        using (var scope = _factory.Services.CreateScope())
        {
            // Pobranie kontekstu bazy danych
            var dbContext = scope.ServiceProvider.GetRequiredService<DataContext>();

            dbContext.Products.RemoveRange(dbContext.Products);

            for (int i = 0; i < 10000; i++)
            {
                dbContext.Products.AddRange(
                    new Product { Name = "Product" + i }
                );

                // Zapisanie obiektu
                dbContext.SaveChanges();
            }
        }

        // Act
        var response = await _client.GetAsync("/api/product");

        // Assert
        response.EnsureSuccessStatusCode();
        var products = await response.Content.ReadFromJsonAsync<List<Product>>();
        Assert.Equal(10000, products?.Count);
    }

    [Fact]
    public async Task Post_AddThousandsProducts_ExceptedThousandsProducts()
    {
        // Arrange
        using (var scope = _factory.Services.CreateScope())
        {
            // Pobranie kontekstu bazy danych
            var dbContext = scope.ServiceProvider.GetRequiredService<DataContext>();

            dbContext.Products.RemoveRange(dbContext.Products);

            for (int i = 0; i < 10000; i++)
            {
                dbContext.Products.AddRange(
                    new Product { Name = "Products" + i }
                );

                // Zapisanie obiektu
                await dbContext.SaveChangesAsync();
            }
        }

        // Act
        var responce = await _client.GetAsync("/api/product");

        // Assert
        responce.EnsureSuccessStatusCode();
        var products = await responce.Content.ReadFromJsonAsync<List<Product>>();
        Assert.Equal(10000, products?.Count);
    }

    [Fact]
    public async Task Add_AddProduct_ExceptedOneProduct()
    {
        using (var scope = _factory.Services.CreateScope())
        {
            // Pobranie contekstu bazy danych
            var dbContext = scope.ServiceProvider.GetRequiredService<DataContext>();

            dbContext.Products.RemoveRange(dbContext.Products);
            dbContext.SaveChanges();

            // Act
            var category = new Category
            {
                Name = "test"
            };

            var product = new Product
            {
                Name = "Product",
                Category = category
            };

            var json = JsonConvert.SerializeObject(product);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PatchAsync("/api/Product", content);

            var result = await dbContext.Products.ToListAsync();

            // Assert
            Assert.Equal(1, result?.Count);
        }
    }
}