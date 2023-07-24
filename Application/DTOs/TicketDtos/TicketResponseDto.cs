using Domain.Entities;

namespace Application.DTOs.TicketDtos
{
    public record TicketResponseDto : Ticket
    {
        public bool? IsChecked { get; set; }

    }
}
