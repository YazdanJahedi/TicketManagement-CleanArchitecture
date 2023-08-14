﻿using Application.Dtos.MessageDtos;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.TicketDtos
{
    public record GetTicketResponse
    {
        public required long FaqCategoryId { get; set; }
        public required string Title { get; set; }
        public required string Status { get; set; }
        public IEnumerable<GetMessageDetailsDto>? Messages { get; set; }
    }
}
