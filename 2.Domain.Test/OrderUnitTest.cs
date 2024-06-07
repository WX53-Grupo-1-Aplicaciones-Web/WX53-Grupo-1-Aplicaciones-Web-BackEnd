using _3.Data;
using _3.Data.Models;
using Moq;

namespace _2.Domain.Test;

public class OrderUnitTest
{
    [Fact]
    public async Task SaveAsync_ThrowsException_WhenShippingDateIsBeforeRequestDate()
    {
        //Arrange
        var mockOrderData = new Mock<IOrderData>();
        var orderDomain = new OrderDomain.OrderDomain(mockOrderData.Object);
        var order = new Order
        {
            shipping_date = DateTime.Now.AddDays(-1),
            request_date = DateTime.Now,
            delivery_address = "Av. Lima 1234",
            status = "Pending"
        };
        
        //Act
        var ex = await Assert.ThrowsAsync<Exception>(() => orderDomain.SaveAsync(order));
        
        //Assert
        Assert.Equal("Shipping date can't be before the request date.", ex.Message);
    }

    [Fact]
    public async Task SaveAsync_ThrowsException_WhenShippingAddressIsEmpty()
    {
        //Arrange
        var mockOrderData = new Mock<IOrderData>();
        var orderDomain = new OrderDomain.OrderDomain(mockOrderData.Object);
        var order = new Order
        {
            shipping_date = DateTime.Now.AddDays(1),
            request_date = DateTime.Now,
            delivery_address = "",
            status = "Pending"
        };
        
        //Act
        var ex = await Assert.ThrowsAsync<ArgumentException>(() => orderDomain.SaveAsync(order));
        
        //Assert
        Assert.Equal("The delivery address can not be empty.", ex.Message);
    }
    
    [Fact]
    public async Task SaveAsync_ThrowsException_WhenStatusIsEmpty()
    {
        // Arrange
        var mockOrderData = new Mock<IOrderData>();
        var orderDomain = new OrderDomain.OrderDomain(mockOrderData.Object);
        var order = new Order
        {
            shipping_date = DateTime.Now.AddDays(1),
            request_date = DateTime.Now,
            delivery_address = "123 Main St",
            status = ""
        };

        // Act
        var ex = await Assert.ThrowsAsync<ArgumentException>(() => orderDomain.SaveAsync(order));
        
        //Assert
        Assert.Equal("Status can not be empty", ex.Message);
    }
    
    [Fact]
    public async Task SaveAsync_SavesOrder_WhenAllDataIsValid()
    {
        //Arrange
        var mockOrderData = new Mock<IOrderData>();
        var orderDomain = new OrderDomain.OrderDomain(mockOrderData.Object);
        var order = new Order
        {
            shipping_date = DateTime.Now.AddDays(1),
            request_date = DateTime.Now,
            delivery_address = "Av. Lima 1234",
            status = "Pending"
        };

        mockOrderData.Setup(data => data.SaveAsync(order)).ReturnsAsync(true);
        
        //Act
        var result = await orderDomain.SaveAsync(order);
        
        //Assert
        Assert.True(result);
    }
}
