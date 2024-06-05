using _3.Data;
using _3.Data.Models;
using Moq;

namespace _2.Domain.Test;

public class ProductUnitTest
{
    [Fact]
    public async Task SaveAsync_ThrowsException_WhenNameIsTooLong()
    {
        // Arrange
        var mockProductData = new Mock<IProductData>();
        var longNameProduct = new Product { Name = new string('A', 51) };
        var productDomain = new ProductDomain.ProductDomain(mockProductData.Object);

        // Act & Assert
        var ex = await Assert.ThrowsAsync<ArgumentException>(() => productDomain.SaveAsync(longNameProduct));
        Assert.Equal("El nombre del producto no puede tener más de 50 caracteres.", ex.Message);
    }

    [Fact]
    public async Task SaveAsync_ThrowsException_WhenUnitPriceIsNegative()
    {
        // Arrange
        var mockProductData = new Mock<IProductData>();
        var negativePriceProduct = new Product { Unit_Price = -1 };
        var productDomain = new ProductDomain.ProductDomain(mockProductData.Object);

        // Act & Assert
        var ex = await Assert.ThrowsAsync<ArgumentException>(() => productDomain.SaveAsync(negativePriceProduct));
        Assert.Equal("El precio unitario del producto debe ser positivo.", ex.Message);
    }

    [Fact]
    public async Task SaveAsync_ThrowsException_WhenUnitPriceHasInvalidFormat()
    {
        // Arrange
        var mockProductData = new Mock<IProductData>();
        var invalidPriceFormatProduct = new Product { Unit_Price = 1.999 };
        var productDomain = new ProductDomain.ProductDomain(mockProductData.Object);

        // Act & Assert
        var ex = await Assert.ThrowsAsync<ArgumentException>(() => productDomain.SaveAsync(invalidPriceFormatProduct));
        Assert.Equal("El precio unitario del producto debe tener un formato decimal válido (hasta dos decimales).", ex.Message);
    }

    [Fact]
    public async Task SaveAsync_ThrowsException_WhenStockIsNegative()
    {
        // Arrange
        var mockProductData = new Mock<IProductData>();
        var negativeStockProduct = new Product { Stock = -1 };
        var productDomain = new ProductDomain.ProductDomain(mockProductData.Object);

        // Act & Assert
        var ex = await Assert.ThrowsAsync<ArgumentException>(() => productDomain.SaveAsync(negativeStockProduct));
        Assert.Equal("El stock del producto no puede ser negativo.", ex.Message);
    }

    [Fact]
    public async Task SaveAsync_ThrowsException_WhenDescriptionIsTooLong()
    {
        // Arrange
        var mockProductData = new Mock<IProductData>();
        var longDescriptionProduct = new Product { Description = new string('A', 301) };
        var productDomain = new ProductDomain.ProductDomain(mockProductData.Object);

        // Act & Assert
        var ex = await Assert.ThrowsAsync<ArgumentException>(() => productDomain.SaveAsync(longDescriptionProduct));
        Assert.Equal("La descripción del producto no puede tener más de 300 caracteres.", ex.Message);
    }

    [Fact]
    public async Task SaveAsync_CreatesProduct_WhenValidProduct()
    {
        // Arrange
        var mockProductData = new Mock<IProductData>();
        var validProduct = new Product 
        { 
            Name = "Valid Product", 
            Unit_Price = 10.99, 
            Stock = 100, 
            Description = "Valid description"
        };
        mockProductData.Setup(data => data.SaveAsync(validProduct)).ReturnsAsync(true);
        var productDomain = new ProductDomain.ProductDomain(mockProductData.Object);

        // Act
        var result = await productDomain.SaveAsync(validProduct);

        // Assert
        Assert.True(result);
    }
}
