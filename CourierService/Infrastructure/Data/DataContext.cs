using CourierService.Core.Domain.Bussiness;

namespace CourierService.Core.Infrastructure.Data
{
    public class DataContext
    {
        public IReadOnlyList<Offer> Offers { get; } = SeedOffers();
        private static IReadOnlyList<Offer> SeedOffers()
        {
            return
            [
                new Offer
                {
                    Id = 1,
                    OfferCode = "OFR001",
                    DiscountPercentage = 10,
                    Criteria = new OfferCritria
                    {
                        MinDistanceInKm = 0,
                        MaxDistanceInKm = 199,
                        MinWeightInKg = 70,
                        MaxWeightInKg = 200
                    }
                },
                new Offer
                {
                    Id = 2,
                    OfferCode = "OFR002",
                    DiscountPercentage = 7,
                    Criteria = new OfferCritria
                    {
                        MinDistanceInKm = 50,
                        MaxDistanceInKm = 150,
                        MinWeightInKg = 100,
                        MaxWeightInKg = 250
                    }
                },
                new Offer
                {
                    Id = 3,
                    OfferCode = "OFR003",
                    DiscountPercentage = 5,
                    Criteria = new OfferCritria
                    {
                        MinDistanceInKm = 50,
                        MaxDistanceInKm = 250,
                        MinWeightInKg = 10,
                        MaxWeightInKg = 150
                    }
                }
            ];
        }
    }
}
