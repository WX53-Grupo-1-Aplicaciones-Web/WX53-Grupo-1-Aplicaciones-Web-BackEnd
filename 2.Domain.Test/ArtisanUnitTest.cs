using _3.Data;
using _3.Data.Models;
using Moq;
namespace _2.Domain.ArtisanDomain.Test
{
    public class ArtisanUnitTest
    {
        [Fact]
        public async Task SaveAsync_ThrowsException_WhenLastNameIsInvalid()
        {
            // Arrange
            var mockArtisanData = new Mock<IArtisanData>();
            var invalidLastNameArtisan = new Artisan { Email = "valid@email.com", Name = "John", LastName = "A", Phone = 123456789 }; 
            var artisanDomain = new ArtisanDomain(mockArtisanData.Object);

            // Act & Assert
            var ex = await Assert.ThrowsAsync<ArgumentException>(() => artisanDomain.SaveAsync(invalidLastNameArtisan));
            Assert.Equal("El apellido debe tener entre 2 y 50 caracteres (Parameter 'LastName')", ex.Message);
        }

        [Fact]
        public async Task SaveAsync_CreatesArtisan_WhenDataIsValid()
        {
            // Arrange
            var mockArtisanData = new Mock<IArtisanData>();
            var validArtisan = new Artisan { Email = "valid@email.com", Name = "John", LastName = "Doe", Phone = 123456789 };
            mockArtisanData.Setup(data => data.GetByEmail(validArtisan.Email)).ReturnsAsync((Artisan)null);
            mockArtisanData.Setup(data => data.GetByPhone(validArtisan.Phone)).ReturnsAsync((Artisan)null);
            mockArtisanData.Setup(data => data.SaveAsync(validArtisan)).ReturnsAsync(true);
            var artisanDomain = new ArtisanDomain(mockArtisanData.Object);

            // Act
            var result = await artisanDomain.SaveAsync(validArtisan);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task UpdateAsync_UpdatesArtisan_WhenDataIsValid()
        {
            // Arrange
            var mockArtisanData = new Mock<IArtisanData>();
            var existingArtisan = new Artisan { Id = 1, Email = "existing@email.com", Name = "John", LastName = "Doe", Phone = 123456789 };
            mockArtisanData.Setup(data => data.GetByIdAsync(existingArtisan.Id)).ReturnsAsync(existingArtisan);
            mockArtisanData.Setup(data => data.UpdateAsync(It.IsAny<Artisan>(), existingArtisan.Id)).ReturnsAsync(true);
            var artisanDomain = new ArtisanDomain(mockArtisanData.Object);

            // Act
            var updatedArtisan = new Artisan { Id = existingArtisan.Id, Email = "updated@email.com", Name = "Updated John", LastName = "Doe", Phone = 123456789 };
            var result = await artisanDomain.UpdateAsync(updatedArtisan, updatedArtisan.Id);

            // Assert
            Assert.True(result);
            mockArtisanData.Verify(data => data.UpdateAsync(It.Is<Artisan>(a => a.Email == updatedArtisan.Email && a.Name == updatedArtisan.Name && a.LastName == updatedArtisan.LastName && a.Phone == updatedArtisan.Phone), updatedArtisan.Id), Times.Once());
        }

        [Fact]
        public async Task DeleteAsync_DeletesArtisan_WhenExists()
        {

            // Arrange
            var mockArtisanData = new Mock<IArtisanData>();
            var existingArtisan = new Artisan { Id = 1, Email = "existing@email.com", Name = "John", LastName = "Doe", Phone = 123456789 };
            mockArtisanData.Setup(data => data.GetByIdAsync(existingArtisan.Id)).ReturnsAsync(existingArtisan);
            mockArtisanData.Setup(data => data.DeleteAsync(existingArtisan.Id)).ReturnsAsync(true);
            var artisanDomain = new ArtisanDomain(mockArtisanData.Object);

            // Act
            var result = await artisanDomain.DeleteAsync(existingArtisan.Id);

            // Assert
            Assert.True(result);
        }
    }
}