using Domain.Common;

namespace Domain.Entities
{
    public record MessageAttachment : BaseEntity
    {
        public required long MessageId { get; set; }
        public required string FileName { get; set; }
        public required string Path { get; set; } 

        public virtual Message? Message { get; set; }
    }
}
