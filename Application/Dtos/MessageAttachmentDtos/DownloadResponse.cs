namespace Application.Dtos.MessageAttachmentDtos
{
    public record DownloadResponse
    {
        public required string FileName { get; set; }
        public required string MimeType { get; set; }
        public required byte[] Data { get; set; }
    }
}
