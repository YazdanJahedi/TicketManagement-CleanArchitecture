namespace Domain.Common
{
    public record BaseEntity
    {
        public long Id { get; set; } 
        public DateTime CreationTime { get; set; }

    }
}
