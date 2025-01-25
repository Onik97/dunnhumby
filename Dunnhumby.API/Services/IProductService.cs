namespace Dunnhumby.API.Services;

using Dunnhumby.API.Models;

public interface IProductService
{
    Task<IEnumerable<Product>> GetAllProductsAsync();
    Task<IResult> AddProductAsync(Product product);
    Task<IResult> GetTotalStockQuantityPerCategoryAsync();
}
