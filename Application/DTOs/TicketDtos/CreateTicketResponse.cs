using Domain.Entities;

namespace Application.DTOs.TicketDtos
{
    public record CreateTicketResponse
    {
        public required string Title { get; set; }
        public required string Description { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
