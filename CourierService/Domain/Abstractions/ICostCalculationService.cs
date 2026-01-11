namespace CourierService.Core.Domain.Abstractions
{
    public interface ICostCalculationService
    {
        /// <summary>
        /// Get delivery cost after applying offer percentage
        /// </summary>
        /// <param name="baseDeliveryCost"></param>
        /// <param name="weightInKg"></param>
        /// <param name="distanceInKm"></param>
        /// <param name="offerPercentage"></param>
        /// <returns>Delivery total Cost and Discounted Cost </returns>
        public (decimal totalCost, decimal discountCost) CalculateDeliveryCost(
            decimal baseDeliveryCost,
            decimal weightInKg,
            decimal distanceInKm,
            decimal offerPercentage);
    }
}
