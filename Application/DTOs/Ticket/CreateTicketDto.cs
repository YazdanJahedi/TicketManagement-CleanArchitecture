namespace Application.DTOs.Ticket
{
    public record CreateTicketDto
    {
        public required string Title { get; set; }
        public required string Description { get; set; }
    }
}
