using _3.Data;
using _3.Data.Models;
using Moq;
namespace _2.Domain.ArtisanDomain.Test
{
    public class ArtisanUnitTest
    {
        [Fact]
        public async Task SaveAsync_ThrowsException_WhenLastNameIsTooLong()
        {
            // Arrange
            var mockArtisanData = new Mock<IArtisanData>();
            var longLastNameArtisan = new Artisan { Email = "valid@email.com", Name = "John", LastName = new string('a', 51) };
            var artisanDomain = new ArtisanDomain(mockArtisanData.Object);

            // Act & Assert
            var ex = await Assert.ThrowsAsync<ArgumentException>(() => artisanDomain.SaveAsync(longLastNameArtisan));
            Assert.Equal("El apellido debe tener entre 2 y 50 caracteres", ex.Message);
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
            mockArtisanData.Setup(data => data.GetByEmail(existingArtisan.Email)).ReturnsAsync(existingArtisan);
            mockArtisanData.Setup(data => data.UpdateAsync(existingArtisan, existingArtisan.Id)).ReturnsAsync(true);
            var artisanDomain = new ArtisanDomain(mockArtisanData.Object);

            // Act
            var updatedArtisan = new Artisan { Id = existingArtisan.Id, Email = "updated@email.com", Name = "Updated John", LastName = "Doe", Phone = 123456789 };
            var result = await artisanDomain.UpdateAsync(updatedArtisan, updatedArtisan.Id);

            // Assert
            Assert.True(result);
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
