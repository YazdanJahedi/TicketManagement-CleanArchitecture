namespace Application.Dtos.MessageAttachmentDtos
{
    public record AttachmentDetailsDto
    {
        public required long Id { get; set; }
        public required string FileName { get; set; }
    }
}
