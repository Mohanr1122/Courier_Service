using CourierService.Core.Application.Abstractions;
using CourierService.Core.Domain.Bussiness;
using CourierService.Core.Domain.Rules;
using CourierService.Core.Infrastructure.Data;

namespace CourierService.Core.Application.Services
{
    public class OfferService : IOfferService
    {
        public readonly IReadOnlyList<Offer> _offers;
        public OfferService() => _offers = new DataContext().Offers;
        public decimal GetOfferPercentage(decimal weightInKg, decimal distanceInKm)
        {
            var applicableOffers = _offers
                .Where(offer =>
                    OfferRule.IsApplicable(offer, weightInKg, distanceInKm));

            return applicableOffers.Any() ? applicableOffers.Max(offer => offer.DiscountPercentage) : 0m;
        }
    }
}
