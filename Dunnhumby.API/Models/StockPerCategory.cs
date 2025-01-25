namespace Dunnhumby.API.Models;

public class StockPerCategory
{
    public string Category { get; set; } = string.Empty;
    public int TotalStock { get; set; }
}