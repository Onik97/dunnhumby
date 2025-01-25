namespace Dunnhumby.API.Repositories;

using Dunnhumby.API.Data;
using Dunnhumby.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

// Will implement integration test this if it was allowed
public class ProductRepository(DunnhumbyContext context) : IProductRepository
{
    public async Task<IEnumerable<Product>> GetAllProductsAsync()
        // Probably needs select() and Skip for pagination
        => await context.Products.AsNoTracking().ToListAsync();

    public async Task AddProductAsync(Product product)
    {
        await context.Products.AddAsync(product);
        await context.SaveChangesAsync();
    }

    public async Task<Product?> GetProductByProductCodeAsync(string productCode)
        => await context.Products.AsNoTracking().FirstOrDefaultAsync(p => p.ProductCode == productCode);

    public async Task<List<StockPerCategory>> GetTotalStockQuantityPerCategoryAsync()
        => await context.Products
                .GroupBy(p => p.Category)
                .Select(g => new StockPerCategory
                {
                    Category = g.Key,
                    TotalStock = g.Sum(p => p.StockQuantity)
                })
                .ToListAsync();
}
