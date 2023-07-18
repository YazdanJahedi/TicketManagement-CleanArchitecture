namespace Application.Features.CreateTicket
{
    public record CreateTicketResponse
    {
        public required string Title { get; set; }
        public required string Description { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
