using EShop.Domain.Models;

namespace EShop.Domain;

public interface IProductService
{
    IEnumerable<Product> GetAll();
    
    Product? GetById(int id);

    void Create();
    void Update();
    void Delete();

}
