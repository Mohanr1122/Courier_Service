namespace CourierService.Core.Domain.Bussiness
{
    public sealed class Offer : BaseEntity
    {
        public string? OfferCode { get; set; }
        public decimal DiscountPercentage { get; set; }
        public OfferCritria? Criteria { get; set; }
    }

    public sealed record OfferCritria
    {
        public long OfferCriteriaId { get; set; }
        public decimal MinDistanceInKm { get; set; }
        public decimal MaxDistanceInKm { get; set; }
        public decimal MinWeightInKg { get; set; }
        public decimal MaxWeightInKg { get; set; }
    }
}
