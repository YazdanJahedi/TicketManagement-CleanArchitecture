﻿using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.TicketFeatures.CreateTicket
{
    public record CreateTicketRequest : IRequest
    {
        public required string Title { get; set; }
        public required string Description { get; set; }
        public required long FaqCatgoryId { get; set; }

        public IEnumerable<IFormFile>? Attachments { get; set; }
    }
}
