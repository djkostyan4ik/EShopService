using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EShop.Domain.Models;

[Table("Products")]
public class Product : BaseModel
{
    [Key]
    //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [StringLength(13, MinimumLength = 8)] // EAN can have from 8 to 13 characters
    public string Ean { get; set; } = string.Empty;

    [Required]
    public decimal Price { get; set; }

    public int Stock { get; set; } = 0;

    public string Sku { get; set; } = string.Empty;

    public Category Category { get; set; } = default!;

}
