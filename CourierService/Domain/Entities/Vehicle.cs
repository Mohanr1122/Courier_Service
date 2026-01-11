namespace CourierService.Core.Domain.Bussiness
{
    public class Vehicle : BaseEntity
    {
        public int MaxLoadInKg { get; set; }
        public decimal SpeedInKmPerHr { get; set; }
        public decimal AvailableAfter { get; set; }
        public List<Package> AssignedPackages { get; } = [];
    }
}
