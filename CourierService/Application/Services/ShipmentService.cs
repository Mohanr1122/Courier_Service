using CourierService.Core.Application.Abstractions;
using CourierService.Core.Domain.Abstractions;
using CourierService.Core.Domain.Bussiness;
using CourierService.Core.Domain.Services;

namespace CourierService.Core.Application.Services
{
    public class ShipmentService : IShipmentService
    {
        private readonly IPackageSelection _packageSelection;
        public ShipmentService() => _packageSelection = new MaxPackageSelectionService();
        public IEnumerable<Vehicle> Dispatch(List<Package> packages, List<Vehicle> vehicles)
        {
            while (packages.Any(p => !p.IsDispatched))
            {
                foreach (var vehicle in vehicles.OrderBy(v => v.AvailableAfter))
                {
                    var applicablePackages = packages
                        .Where(x => x.WeightInKg <= vehicle.MaxLoadInKg && !x.IsDispatched);

                    var selectedPackages = _packageSelection.GetPackages(
                        [.. applicablePackages.Select(x => (x.Id, x.WeightInKg))],
                        vehicle.MaxLoadInKg);

                    if (!selectedPackages.Any())
                        continue;
                    var shipments = applicablePackages.Where(x => selectedPackages.Contains(x.Id));
                    AssignShipment(vehicle, [.. shipments]);
                }
            }
            return vehicles;
        }
        private static Vehicle AssignShipment(Vehicle vehicle, List<Package> shipments)
        {
            foreach (var package in shipments)
            {
                package.IsDispatched = true;
                package.EstimatedDeliveryInHrs =
                    Math.Round(package.DistanceInKm / vehicle.SpeedInKmPerHr, 2)
                    + vehicle.AvailableAfter;
            }

            var maxTravelTime = shipments.Max(p => p.DistanceInKm) / vehicle.SpeedInKmPerHr;
            vehicle.AvailableAfter += Math.Round(2 * maxTravelTime, 2);
            vehicle.AssignedPackages.AddRange(shipments);
            return vehicle;
        }
    }
}
