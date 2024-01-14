using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DianaApp.ViewModels.ProductVM;
public class ProductCreateVM
{
    [MaxLength(64)]
    public string Name { get; set; }
    [MaxLength(128)]
    public string? Description { get; set; }
    [MaxLength(128)]
    public string? About { get; set; }
    [Column(TypeName = "smallmoney")]
    public decimal SellPrice { get; set; }
    [Column(TypeName = "smallmoney")]
    public decimal CostPrice { get; set; }
    [Range(0, 100)]
    public float Discount { get; set; }
    public ushort Quantity { get; set; }
    public IFormFile Image { get; set; }
    public int? CategoryId { get; set; }
}
