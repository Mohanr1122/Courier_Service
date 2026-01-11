using CourierService.Core.Domain.Abstractions;
using CourierService.Core.Domain.Common;

namespace CourierService.Core.Domain.Services
{
    public class CostCalculationService : ICostCalculationService
    {
        public (decimal totalCost, decimal discountCost) CalculateDeliveryCost(
            decimal baseDeliveryCost,
            decimal weightInKg,
            decimal distanceInKm,
            decimal offerPercentage)
        {
            var totalCost = CalculateDeliveryCost(
                baseDeliveryCost,
                weightInKg,
                distanceInKm);
            var discountCost = (totalCost * offerPercentage / Constants.PERCENTAGE);
            var discountedCost = totalCost - discountCost;
            return (totalCost: discountedCost, discountCost);
        }

        private static decimal CalculateDeliveryCost(
            decimal baseDeliveryCost,
            decimal weightInKg,
            decimal distanceInKm) =>
         baseDeliveryCost + (Constants.COST_PER_KG * weightInKg) + (Constants.COST_PER_KM * distanceInKm);
    }
}
