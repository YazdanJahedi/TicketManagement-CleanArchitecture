namespace Domain.Common
{
    public record BaseEntity
    {
        public required long Id { get; set; } 
        public DateTime CreationDate { get; set; }
    }
}
