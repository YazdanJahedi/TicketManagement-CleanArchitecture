namespace Domain.Common
{
    public abstract record BaseEntity
    {
        public long Id { get; set; }
        public DateTime CreationTime { get; set; }

    }
}
