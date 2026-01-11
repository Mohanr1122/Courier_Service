namespace CourierService.Core.Presentation.ViewModels
{
    public record class PackageVm
    {
        public int Id { get; set; }
        public required int WeightInKg { get; set; }
        public required decimal DistanceInKm { get; set; }
    }
}
