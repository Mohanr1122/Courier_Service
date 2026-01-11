namespace CourierService.Core.Application.Abstractions
{
    public interface IOfferService
    {
        /// <summary>
        /// Get applicable offer Percentage based on weight and distance
        /// </summary>
        /// <param name="weightInKg"></param>
        /// <param name="distanceInKm"></param>
        /// <returns></returns>
        public decimal GetOfferPercentage(decimal weightInKg, decimal distanceInKm);
    }
}
