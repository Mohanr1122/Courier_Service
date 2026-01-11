using System.Collections.Generic;
using System.Linq;
using CourierService.Core.Domain.Services;
using Xunit;

namespace CourierService.Core.Tests
{
    public class MaxPackageSelectionServiceTests
    {
        // exact fit scenario
        [Fact]
        public void Selects_Exact_Return_Max_Packages()
        {
            // Arrange
            var service = CreateService();
            var values = new List<(long Id, int Weight)> { (1, 50), (2, 75), (3, 175), (4, 110), (5, 155) };

            // Act
            var selected = service.GetPackages(values, 200).ToList();

            // Assert
            Assert.Contains(2, selected);
            Assert.Contains(4, selected);
            Assert.DoesNotContain(3, selected);
        }

        // no package fits
        [Fact]
        public void Returns_Empty_When_No_Package_Fits()
        {
            // Arrange
            var service = CreateService();
            var values = new List<(long Id, int Weight)> { (1, 120), (2, 130) };

            // Act
            var selected = service.GetPackages(values, 100).ToList();

            // Assert
            Assert.Empty(selected);
        }

        // multiple combinations may reach same weight; ensure sum is maximized
        [Fact]
        public void Maximizes_Total_Weight()
        {
            // Arrange
            var service = CreateService();
            var values = new List<(long Id, int Weight)>
            {
                (1, 40), (2, 40), (3, 30), (4, 10)
            };

            // Act
            var selected = service.GetPackages(values, 80).ToList();

            // Assert
            // best total weight is 80
            Assert.True(selected.Sum(id => values.First(v => v.Id == id).Weight) == 80);
        }

        private static MaxPackageSelectionService CreateService() => new();
    }
}
