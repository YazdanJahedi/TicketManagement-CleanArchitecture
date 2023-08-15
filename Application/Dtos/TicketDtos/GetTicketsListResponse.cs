using Application.Dtos.UserDtos;

namespace Application.Dtos.TicketDtos
{
    public record GetTicketsListResponse
    {
        public long Id { get; set; }
        public required UserDetailsDto Creator { get; set; }
        public required string Title { get; set; }
        public DateTime? FirstResponseDate { get; set; }
        public required string Status { get; set; }
        public DateTime? CloseDate { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
