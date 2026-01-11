using System.Collections.Generic;
using System.Linq;
using CourierService.Core.Application.Handler;
using CourierService.Core.Presentation.ViewModels;
using Xunit;

namespace CourierService.Core.Tests
{
    public class ShipmentHandlerTests
    {
        [Fact]
        public void HandleShipment_Throws_On_Invalid_BaseCost()
        {
            // Arrange
            var handler = CreateHandler();
            var packageVms = new List<PackageVm>
            {
                new() { Id = 1, WeightInKg = 10, DistanceInKm = 10m }
            };

            // Act & Assert
            Assert.Throws<System.ArgumentException>(() =>
                handler.HandleShipment(0m, packageVms, new VechicleVm()).ToList());
        }

        [Fact]
        public void HandleShipment_Throws_On_Empty_Packages()
        {
            // Arrange
            var handler = CreateHandler();
            var packageVms = new List<PackageVm>();

            // Act & Assert
            Assert.Throws<System.ArgumentException>(() =>
                handler.HandleShipment(100m, packageVms, new VechicleVm()).ToList());
        }
        [Fact]
        public void HandleShipment_Throws_On_Null_Packages()
        {
            // Arrange
            var handler = CreateHandler();
            List<PackageVm> packageVms = null;

            // Act & Assert
            Assert.Throws<System.ArgumentException>(() =>
                handler.HandleShipment(100m, packageVms, new VechicleVm()).ToList());
        }


        [Fact]
        public void HandleShipment_Returns_Calculated_Vechicles()
        {
            // Arrange
            var handler = CreateHandler();
            var packageVms = new List<PackageVm>
            {
                new() { Id = 1, WeightInKg = 50, DistanceInKm = 30m },
                new() { Id = 2, WeightInKg = 70, DistanceInKm = 125m },
                new() { Id = 3, WeightInKg = 175, DistanceInKm = 100m },
                new() { Id = 4, WeightInKg = 110, DistanceInKm = 60m },
                new() { Id = 5, WeightInKg = 155, DistanceInKm = 95m }
            };
            var vechicleVm = new VechicleVm
            {
                MaxSpeed = 70,
                Maxload = 200,
                NoOfVechicles = 2
            };

            // Act
            var result = handler.HandleShipment(100m, packageVms, vechicleVm).ToList();

            // Assert
            Assert.Equal(5, result.Count);
            Assert.All(result, p => Assert.True(p.TotalAmount >= 0m));
            // Ensure all packages have dispatched
            Assert.All(result, p => Assert.True(p.IsDispatched));
            // Ensure estimated delivery times are calculated and non-negative
            Assert.All(result, p => Assert.True(p.EstimatedDeliveryInHrs >= 0m));            
        }

        private static ShipmentHandler CreateHandler() => new();
    }
}
