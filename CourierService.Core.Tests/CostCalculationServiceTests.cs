using CourierService.Core.Domain.Services;
using Xunit;

namespace CourierService.Core.Tests
{
    public class CostCalculationServiceTests
    {
        [Fact]
        public void Calculates_Correct_Costs_For_No_Offer()
        {
            // Arrange
            var svc = CreateService();

            // Act
            var (total, discount) = svc.CalculateDeliveryCost(100m, 2m, 3m, 0m);

            // Assert
            // base 100 + (2*10) + (3*5) = 100 + 20 + 15 = 135
            Assert.Equal(0m, discount);
            Assert.Equal(135m, total);
        }

        [Fact]
        public void Applies_Full_Discount_Correctly()
        {
            // Arrange
            var svc = CreateService();

            // Act
            var (total, discount) = svc.CalculateDeliveryCost(50m, 1m, 1m, 100m);

            // Assert
            // base 50 + 10 + 5 = 65 -> 100% discount -> total 0, discount 65
            Assert.Equal(65m, discount);
            Assert.Equal(0m, total);
        }

        // helper to create service instance
        private static CostCalculationService CreateService() => new();
    }
}
