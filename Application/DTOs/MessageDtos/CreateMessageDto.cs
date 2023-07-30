namespace Application.DTOs.MessageDtos
{
    public record CreateMessageDto
    {
        public required string Text { get; set; }

    }
}
