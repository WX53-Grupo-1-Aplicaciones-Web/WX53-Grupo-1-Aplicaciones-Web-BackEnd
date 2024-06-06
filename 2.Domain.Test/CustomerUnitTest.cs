using _3.Data;
using _3.Data.Models;
using Moq;

namespace _2.Domain.Test;

public class CustomerUnitTest
{
    [Fact]
    public async Task SaveAsync_ThrowsException_WhenEmailAlreadyExists()
    {
        // Arrange
        var mockCustomerData = new Mock<ICustomerData>();
        var existingCustomer = new Customer { Email = "existing@email.com", Phone = 999888777 }; 
        mockCustomerData.Setup(data => data.GetByEmail(existingCustomer.Email)).ReturnsAsync(existingCustomer);
        var customerDomain = new CustomerDomain(mockCustomerData.Object);

        // Act
        var newCustomer = new Customer { Email = existingCustomer.Email, Phone = 999888777}; 

        // Assert
        var ex = await Assert.ThrowsAsync<Exception>(() => customerDomain.SaveAsync(newCustomer));
        Assert.Equal("Correo vinculado con una cuenta existente", ex.Message);
    }
    
    [Fact]
    public async Task SaveAsync_ThrowsException_WhenEmailIsInvalid()
    {
        // Arrange
        var mockCustomerData = new Mock<ICustomerData>();
        var invalidEmailCustomer = new Customer { Email = "invalidEmail", Phone = 12345678};
        var customerDomain = new CustomerDomain(mockCustomerData.Object);

        // Act & Assert
        var ex = await Assert.ThrowsAsync<Exception>(() => customerDomain.SaveAsync(invalidEmailCustomer));
        Assert.Equal("El correo electrónico no tiene un formato válido", ex.Message);
    }
    
    [Fact]
    public async Task SaveAsync_ThrowsException_WhenPhoneIsInvalid()
    {
        // Arrange
        var mockCustomerData = new Mock<ICustomerData>();
        var invalidPhoneCustomer = new Customer { Email = "valid@email.com", Phone = 123456789 }; 
        var customerDomain = new CustomerDomain(mockCustomerData.Object);

        // Act & Assert
        var ex = await Assert.ThrowsAsync<ArgumentException>(() => customerDomain.SaveAsync(invalidPhoneCustomer));
        Assert.Equal("El número de teléfono no tiene un formato válido (Parameter 'Phone')", ex.Message);
    }
    [Fact]
    public async Task SaveAsync_ReturnsTrue_WhenEmailAndPhoneAreValid()
    {
        // Arrange
        var mockCustomerData = new Mock<ICustomerData>();
        var validCustomer = new Customer { Email = "valid@email.com", Phone = 912345678 };
        mockCustomerData.Setup(data => data.GetByEmail(validCustomer.Email)).ReturnsAsync((Customer)null);
        mockCustomerData.Setup(data => data.SaveAsync(validCustomer)).ReturnsAsync(true);
        var customerDomain = new CustomerDomain(mockCustomerData.Object);

        // Act
        var result = await customerDomain.SaveAsync(validCustomer);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task SaveAsync_ThrowsNoException_WhenEmailAndPhoneAreValid()
    {
        // Arrange
        var mockCustomerData = new Mock<ICustomerData>();
        var validCustomer = new Customer { Email = "valid@email.com", Phone = 912345678 };
        mockCustomerData.Setup(data => data.GetByEmail(validCustomer.Email)).ReturnsAsync((Customer)null);
        mockCustomerData.Setup(data => data.SaveAsync(validCustomer)).ReturnsAsync(true);
        var customerDomain = new CustomerDomain(mockCustomerData.Object);

        // Act & Assert
        var ex = Record.ExceptionAsync(() => customerDomain.SaveAsync(validCustomer));

        // Assert
        Assert.Null(ex.Result);
    }
}