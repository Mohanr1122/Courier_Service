namespace CourierService.Core.Domain
{
    public abstract class BaseEntity
    {
        public required long Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime ModifiedAt { get; set; } = DateTime.UtcNow;
    }
}
