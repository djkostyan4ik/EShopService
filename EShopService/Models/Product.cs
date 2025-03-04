namespace EShopService.Models;

public class Product
{
    public int Id { get; set; }

    public string Name { get; set; } = default!;

    public string Ean { get; set; } = default!;

    public decimal Price { get; set; }

    public string Sku { get; set; } = default!;

    public Category Category { get; set; } = default!;

    public bool Deleted { get; set; } = false;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public Guid CreatedBy { get; set; }

    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public Guid UpdatedBy { get; set; }

}
