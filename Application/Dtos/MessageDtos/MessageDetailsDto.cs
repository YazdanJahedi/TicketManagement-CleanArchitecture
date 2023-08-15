using Application.Dtos.MessageAttachmentDtos;
using Application.Dtos.UserDtos;

namespace Application.Dtos.MessageDtos
{
    public record MessageDetailsDto
    {
        public required string Text { get; set; }
        public required UserDetailsDto Creator { get; set; }
        public IEnumerable<AttachmentDetailsDto>? Attachments { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
