using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.MessageAttachmentDtos
{
    public record GetAttachmentDetailsDto
    {
        public required long Id { get; set; }
        public required string FileName { get; set; }
    }
}
