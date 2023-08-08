using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.DTOs
{
    public record GetAttachmentDetailsDto
    {
        public required long Id { get; set; }
        public required String FileName { get; set; }
    }
}
