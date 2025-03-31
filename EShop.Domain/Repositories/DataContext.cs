using Microsoft.EntityFrameworkCore;
using EShop.Domain.Models;

namespace EShop.Domain.Repositories;

public class DataContext : DbContext
{

    public DbSet<Product> Products { get; set; }

    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
        
    }

}
