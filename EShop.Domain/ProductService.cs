using EShop.Domain.Models;
using EShop.Domain.Repositories;

namespace EShop.Domain;

public class ProductService 
{
    private readonly IProductRepository _repository;

    public ProductService(IProductRepository repository)
    {
        _repository = repository;
    }

    public IEnumerable<Product> GetAll()
    {
        return _repository.GetAll();
    }

    public Product? GetById(int id) => _repository.GetById(id);

    public void AddProduct(Product product)
    {
        if (product == null) throw new ArgumentNullException(nameof(product));
        if (_repository.Exists(product.Id)) throw new InvalidOperationException("Produkt o tym ID już istnieje.");

        _repository.Add(product);
    }

    public void UpdateProduct(Product product)
    {
        if (product == null) throw new ArgumentNullException(nameof(product));
        if (!_repository.Exists(product.Id)) throw new InvalidOperationException("Produkt o tym ID nie istnieje.");

        _repository.Update(product);
    }

    public void DeleteProduct(int id)
    {
        if (!_repository.Exists(id)) throw new InvalidOperationException("Produkt o tym ID nie istnieje.");

        _repository.Delete(id);
    }
}
