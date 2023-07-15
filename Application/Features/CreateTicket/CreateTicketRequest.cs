namespace Application.Features.CreateTicket
{
    public record CreateTicketRequest
    {
        public required string Title { get; set; }
        public required string Description { get; set; }
    }
}
