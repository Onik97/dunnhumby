namespace Dunnhumby.API.Services;

using Dunnhumby.API.Models;
using Dunnhumby.API.Providers;
using Dunnhumby.API.Repositories;

public class ProductService(IProductRepository productRepository, IDateTimeProvider dateTimeProvider) : IProductService
{
    public async Task<IResult> AddProductAsync(Product product)
    {
        // Simple Validation - But normally will use FluentValidation
        // Perhaps to throw validation so middleware catches it and handles it accordingly
        var errors = new List<string>();

        if (string.IsNullOrEmpty(product.Category)) errors.Add("Product must have a category");
        if (string.IsNullOrEmpty(product.Name)) errors.Add("Product must have a name");
        if (string.IsNullOrEmpty(product.ProductCode)) errors.Add("Product must have a Product Code");
        if (product.Price <= 0) errors.Add("Product price must be valid");
        if (product.StockQuantity <= 0) errors.Add("Stock Quantity must be valid");

        var productFromDb = await productRepository.GetProductByProductCodeAsync(product.ProductCode);
        if (productFromDb != null) errors.Add("Product code must be unique");

        if (errors.Any())
        {
            return Results.BadRequest(errors);
        }

        product.DateAdded = dateTimeProvider.GetUTCDate();
        await productRepository.AddProductAsync(product);

        // May need DTO here, as right now DB ID is being revealed on the API
        return Results.Created($"/api/products/{product.ProductId}", product);
    }

    public async Task<IEnumerable<Product>> GetAllProductsAsync()
        // Kept it simple, probably add some pagination and select() to handle db strain
        => await productRepository.GetAllProductsAsync();

    public async Task<IResult> GetTotalStockQuantityPerCategoryAsync()
    {
        var stockQuantityPerCategory = await productRepository.GetTotalStockQuantityPerCategoryAsync();
        return Results.Ok(stockQuantityPerCategory);
    }
}
