namespace Dunnhumby.API.UnitTesting;

using Dunnhumby.API.Models;
using Dunnhumby.API.Providers;
using Dunnhumby.API.Repositories;
using Dunnhumby.API.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Moq;

public class ProductServiceTests
{
    private Mock<IProductRepository> mockProductRepository;
    private Mock<IDateTimeProvider> mockDateTimeProvider;
    private ProductService sut; // Service Under Test

    [SetUp]
    public void Setup()
    {
        mockProductRepository = new Mock<IProductRepository>();
        mockDateTimeProvider = new Mock<IDateTimeProvider>();
        sut = new ProductService(mockProductRepository.Object, mockDateTimeProvider.Object);
    }

    [TestCase("", "ValidName", "ValidCode", 10.99, 5, "Product must have a category")]
    [TestCase("ValidCategory", "", "ValidCode", 10.99, 5, "Product must have a name")]
    [TestCase("ValidCategory", "ValidName", "", 10.99, 5, "Product must have a Product Code")]
    [TestCase("ValidCategory", "ValidName", "ValidCode", 0, 5, "Product price must be valid")]
    [TestCase("ValidCategory", "ValidName", "ValidCode", -1, 5, "Product price must be valid")]
    [TestCase("ValidCategory", "ValidName", "ValidCode", 10.99, 0, "Stock Quantity must be valid")]
    [TestCase("ValidCategory", "ValidName", "ValidCode", 10.99, -5, "Stock Quantity must be valid")]
    public async Task AddProductAsync_InvalidField_ReturnsErrorMessage(string category,
                                                                       string name,
                                                                       string productCode,
                                                                       decimal price,
                                                                       int stock,
                                                                       string expectedMessage)
    {
        // Arrange
        mockProductRepository.Setup(r => r.GetProductByProductCodeAsync(It.IsAny<string>())).ReturnsAsync((Product?)null);
        var invalidProduct = new Product
        {
            Category = category,
            Name = name,
            ProductCode = productCode,
            Price = price,
            StockQuantity = stock
        };

        // Act
        var result = await sut.AddProductAsync(invalidProduct);

        // Assert
        Assert.That(result, Is.InstanceOf<BadRequest<List<string>>>());

        var badRequest = (BadRequest<List<string>>)result;
        Assert.That(badRequest.Value, Has.Count.EqualTo(1), "Should return exactly one error");
        Assert.That(badRequest.Value!.First(), Is.EqualTo(expectedMessage));
    }

    [Test]
    public async Task AddProductAsync_DuplicateProductCode_ReturnsBadRequest()
    {
        // Arrange
        var duplicateProduct = new Product
        {
            Category = "Electronics",
            Name = "Phone",
            ProductCode = "P123",
            Price = 999.99m,
            StockQuantity = 10
        };

        mockProductRepository.Setup(r => r.GetProductByProductCodeAsync("P123")).ReturnsAsync(new Product()); // Existing product

        // Act
        var result = await sut.AddProductAsync(duplicateProduct);

        // Assert
        Assert.That(result, Is.InstanceOf<BadRequest<List<string>>>());

        var badRequest = (BadRequest<List<string>>)result;
        Assert.That(badRequest.Value, Has.Count.EqualTo(1), "Should return exactly one error");
        Assert.That(badRequest.Value!.First(), Is.EqualTo("Product code must be unique"));
    }

    [Test]
    public async Task AddProductAsync_ValidProduct_ReturnsCreatedWithProduct()
    {
        // Arrange
        var validProduct = new Product
        {
            Category = "Electronics",
            Name = "Phone",
            ProductCode = "P123",
            Price = 999.99m,
            StockQuantity = 10
        };

        var fixedDate = new DateTime(2023, 10, 1);
        mockDateTimeProvider.Setup(d => d.GetUTCDate()).Returns(fixedDate);
        mockProductRepository.Setup(r => r.GetProductByProductCodeAsync("P123")).ReturnsAsync((Product?)null);

        // Act
        var result = await sut.AddProductAsync(validProduct);

        // Assert

        Assert.That(result, Is.InstanceOf<Created<Product>>());

        var createdResult = (Created<Product>)result;
        Assert.That(createdResult.Location, Is.EqualTo($"/api/products/{validProduct.ProductId}"));

        var returnedProduct = createdResult.Value!;
        Assert.That(returnedProduct.Category, Is.EqualTo(validProduct.Category));
        Assert.That(returnedProduct.Name, Is.EqualTo(validProduct.Name));
        Assert.That(returnedProduct.ProductCode, Is.EqualTo(validProduct.ProductCode));
        Assert.That(returnedProduct.Price, Is.EqualTo(validProduct.Price));
        Assert.That(returnedProduct.StockQuantity, Is.EqualTo(validProduct.StockQuantity));
        Assert.That(returnedProduct.DateAdded, Is.EqualTo(fixedDate));

        // Verify repository call
        mockProductRepository.Verify(r => r.AddProductAsync(validProduct), Times.Once);
    }
}