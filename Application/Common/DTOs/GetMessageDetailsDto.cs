﻿using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.DTOs
{
    public record GetMessageDetailsDto
    {
        public required string Text { get; set; }
        public required GetUserInformationDto Creator { get; set; }
        public IEnumerable<GetAttachmentDetailsDto>? Attachments { get; set; }
        public DateTime CreationDate { get; set; }
    }
}