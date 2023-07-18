namespace Application.Features.CreateMessage
{
    public record CreateMessageResponse
    {
        public required string Text { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
