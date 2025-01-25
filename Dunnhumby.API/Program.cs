namespace Dunnhumby.API;

using Dunnhumby.API.Data;
using Dunnhumby.API.Models;
using Dunnhumby.API.Providers;
using Dunnhumby.API.Repositories;
using Dunnhumby.API.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddScoped<IProductRepository, ProductRepository>();
        builder.Services.AddScoped<IProductService, ProductService>();
        builder.Services.AddTransient<IDateTimeProvider, DateTimeProvider>();

        builder.Services.AddDbContext<DunnhumbyContext>(o => o.UseSqlite(builder.Configuration.GetConnectionString("Default")));

        var app = builder.Build();
        app.Urls.Add("http://0.0.0.0:5101"); // Definitely add a environment variable if they want to change the PORT

        // Production mode should use wwwroot
        // Development relies on the vite running in a different port
        if (app.Environment.IsProduction())
        {
            app.UseDefaultFiles();
            app.UseStaticFiles();
        }

        app.MapGet("/api/products", async (IProductService service) => await service.GetAllProductsAsync());
        app.MapPost("/api/products", async (Product product, IProductService service) => await service.AddProductAsync(product));
        app.MapGet("/api/products/stockcategory", async (IProductService service) => await service.GetTotalStockQuantityPerCategoryAsync());

        app.Run();
    }
}