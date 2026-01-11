using System.Collections.Generic;
using System.Linq;
using CourierService.Core.Application.Services;
using CourierService.Core.Domain.Bussiness;
using Xunit;

namespace CourierService.Core.Tests
{
    public class ShipmentServiceTests
    {
        [Fact]
        public void Dispatch_Distributes_Packages_Among_Multiple_Vehicles()
        {
            // Arrange
            var svc = CreateService();

            var packages = new List<Package>
            {
                new () { Id = 1, PackageCode = "PKG_1", WeightInKg = 70, DistanceInKm = 10m },
                new () { Id = 2, PackageCode = "PKG_2", WeightInKg = 70, DistanceInKm = 20m },
                new () { Id = 3, PackageCode = "PKG_3", WeightInKg = 70, DistanceInKm = 30m }
            };

            var vehicles = new List<Vehicle>
            {
                new () { Id = 1, MaxLoadInKg = 200, SpeedInKmPerHr = 70m },
                new () { Id = 2, MaxLoadInKg = 200, SpeedInKmPerHr = 70m }
            };

            // Act
            var dispatched = svc.Dispatch(packages, vehicles).ToList();

            // Assert
            Assert.Equal(2, dispatched.Count);
            Assert.All(packages, p => Assert.True(p.IsDispatched));
            Assert.True(dispatched.Sum(v => v.AssignedPackages.Count) == 3);
        }
        [Fact]
        public void Dispatch_Heavy_Packages_Among_Other_Vehicles()
        {
            // Arrange
            var svc = CreateService();

            var packages = new List<Package>
            {
                new () { Id = 1, PackageCode = "PKG_1", WeightInKg = 70, DistanceInKm = 10m },
                new () { Id = 2, PackageCode = "PKG_2", WeightInKg = 170, DistanceInKm = 20m },
                new () { Id = 3, PackageCode = "PKG_3", WeightInKg = 70, DistanceInKm = 30m }
            };

            var vehicles = new List<Vehicle>
            {
                new () { Id = 1, MaxLoadInKg = 200, SpeedInKmPerHr = 70m },
                new () { Id = 2, MaxLoadInKg = 200, SpeedInKmPerHr = 70m }
            };

            // Act
            var dispatched = svc.Dispatch(packages, vehicles).ToList();

            // Assert
            Assert.Equal(2, dispatched[1].Id);
            Assert.All(packages, p => Assert.True(p.IsDispatched));            
        }


        private static ShipmentService CreateService() => new();
    }
}
