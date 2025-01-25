namespace Dunnhumby.API.Repositories;

using Dunnhumby.API.Models;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAllProductsAsync();
    Task AddProductAsync(Product product);
    Task<Product?> GetProductByProductCodeAsync(string productCode);
    Task<List<StockPerCategory>> GetTotalStockQuantityPerCategoryAsync();
}