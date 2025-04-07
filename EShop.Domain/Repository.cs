using EShop.Domain.Models;
using EShop.Domain.Seeders;
using Microsoft.EntityFrameworkCore;

namespace EShop.Domain
{

    public class Repository : IProductRepository
    {
        private readonly List<Product> _products;

        public Repository()
        {
            _products = EShopSeeder.GetInitialProducts();
        }

        public IEnumerable<Product> GetAll()
        {
            return _products;
        }

        public Product? GetById(int id)
        {
            return _products.FirstOrDefault(p => p.Id == id);
        }

        public void Add(Product product)
        {
            product.Id = _products.Max(p => p.Id) + 1;
            _products.Add(product);
        }

        public void Update(Product product)
        {
            var index = _products.FindIndex(p => p.Id == product.Id);
            if (index != -1)
            {
                _products[index] = product;
            }
        }

        public void Delete(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product != null)
            {
                _products.Remove(product);
            }
        }

        public bool Exists(int id)
        {
            return _products.Any (p => p.Id == id);
        }
    }
}
