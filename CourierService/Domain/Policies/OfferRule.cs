using CourierService.Core.Domain.Bussiness;

namespace CourierService.Core.Domain.Rules
{
    public class OfferRule
    {
        public static readonly Func<Offer, decimal, decimal, bool> IsApplicable = (offer, weightInKg, distanceInKm) =>
            weightInKg >= offer.Criteria!.MinWeightInKg &&
            weightInKg <= offer.Criteria.MaxWeightInKg &&
            distanceInKm >= offer.Criteria.MinDistanceInKm &&
            distanceInKm <= offer.Criteria.MaxDistanceInKm;
    }
}
