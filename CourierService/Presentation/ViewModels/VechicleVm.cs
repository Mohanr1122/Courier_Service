namespace CourierService.Core.Presentation.ViewModels
{
    public record VechicleVm
    {
        public int NoOfVechicles { get; set; }
        public decimal MaxSpeed { get; set; }
        public int Maxload { get; set; }
    }
}
