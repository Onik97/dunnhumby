namespace Dunnhumby.API.Models;

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public class Product
{
    [Key] // Primary key
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Allow Auto Gene and identity for unique
    public int ProductId { get; set; }

    [Required]
    public string Category { get; set; } = string.Empty;

    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    public string ProductCode { get; set; } = string.Empty;

    [Required]
    [Column(TypeName = "decimal(18,2)")] // 18 Digits might be overkill, Decimal for money (Base 2 vs Base 10)
    public decimal Price { get; set; }

    [Required]
    public int StockQuantity { get; set; }

    public DateTime DateAdded { get; set; }
}
