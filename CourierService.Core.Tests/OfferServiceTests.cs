using CourierService.Core.Application.Services;
using Xunit;

namespace CourierService.Core.Tests
{
    public class OfferServiceTests
    {
        [Fact]
        public void Returns_Zero_When_No_Offers_Applicable()
        {
            // Arrange
            var service = CreateService();

            // Act
            var percent = service.GetOfferPercentage(500m, 500m);

            // Assert
            Assert.Equal(0m, percent);
        }

        [Fact]
        public void Picks_Highest_Discount_When_Multiple_Apply()
        {
            // Arrange
            var service = CreateService();

            // Act
            var percent = service.GetOfferPercentage(100m, 100m);

            // Assert
            Assert.Equal(10m, percent);
        }

        private static OfferService CreateService() => new();
    }
}
