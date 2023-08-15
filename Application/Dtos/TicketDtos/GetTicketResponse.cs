using Application.Dtos.MessageDtos;

namespace Application.Dtos.TicketDtos
{
    public record GetTicketResponse
    {
        public required long FaqCategoryId { get; set; }
        public required string Title { get; set; }
        public required string Status { get; set; }
        public IEnumerable<MessageDetailsDto>? Messages { get; set; }
    }
}
