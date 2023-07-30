using Domain.Entities;

namespace Application.DTOs.TicketDtos
{
    public record TicketResponseDto : Ticket
    {
        public bool? IsChecked { get; set; }
        //public DateTime? CloseDate { get; set; }
        // status  -> checked / not checked / closed
        // number of messages
    }
}
