using CourierService.Core.Application.Abstractions;
using CourierService.Core.Application.Services;
using CourierService.Core.Domain.Abstractions;
using CourierService.Core.Domain.Bussiness;
using CourierService.Core.Domain.Services;
using CourierService.Core.Presentation.ViewModels;

namespace CourierService.Core.Application.Handler
{
    public class ShipmentHandler
    {
        private readonly IOfferService _offerService;
        private readonly ICostCalculationService _costCalculationService;
        private readonly IShipmentService _shipmentService;
        public ShipmentHandler()
        {
            _offerService = new OfferService();
            _costCalculationService = new CostCalculationService();
            _shipmentService = new ShipmentService();
        }

        public IEnumerable<Package> HandleShipment(
            decimal baseDeliveryCost,
            IReadOnlyList<PackageVm> packageVms,
            VechicleVm vechicleVm)
        {
            ValidateShipment(baseDeliveryCost, packageVms);
            var packages = packageVms.Select((x, index) => MapPackage(x));

            var vechiles = Enumerable.Range(1, vechicleVm.NoOfVechicles)
                .Select(x => new Vehicle { Id = x, MaxLoadInKg = vechicleVm.Maxload, SpeedInKmPerHr = vechicleVm.MaxSpeed });

            // Dispatch maximum packages to vechiles 
            var AllocatedVechiles = _shipmentService.Dispatch([.. packages], [.. vechiles]);

            foreach (var package in AllocatedVechiles
                .SelectMany(x => x.AssignedPackages)
                .OrderBy(x => x.Id))
            {
                var weightInKg = package.WeightInKg;
                var distanceInKm = package.DistanceInKm;
                var offerPercentage = _offerService.GetOfferPercentage(weightInKg, distanceInKm);
                var (totalCost, discountCost) = _costCalculationService.CalculateDeliveryCost(
                    baseDeliveryCost,
                    weightInKg,
                    distanceInKm,
                    offerPercentage);
                package.TotalAmount = totalCost;
                package.DiscountAmount = discountCost;
                yield return package;
            }
        }

        private static Package MapPackage(PackageVm x)
        => new()
        {
            DistanceInKm = x.DistanceInKm,
            WeightInKg = x.WeightInKg,
            Id = x.Id,
            PackageCode = $"PKG_{x.Id}"
        };

        private static void ValidateShipment(
            decimal baseDeliveryCost,
            IReadOnlyList<PackageVm> packageVms)
        {
            if (baseDeliveryCost <= 0)
            {
                throw new ArgumentException("Base delivery cost must be greater than zero.");
            }
            if (packageVms is null || !packageVms.Any())
            {
                throw new ArgumentException("At least one package must be provided.");
            }
        }

    }
}
