namespace CourierService.Core.Domain.Bussiness
{
    public class Package : BaseEntity
    {
        public required string? PackageCode { get; set; }
        public required int WeightInKg { get; set; }
        public required decimal DistanceInKm { get; set; }
        public bool IsDispatched { get; set; }
        public decimal EstimatedDeliveryInHrs { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal DiscountAmount { get; set; }
    }
}
