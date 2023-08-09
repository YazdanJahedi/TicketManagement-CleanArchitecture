using Application.Common.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.TicketFeatures.GetTicketsList
{
    public record GetTicketsListResponse
    {
        public long Id { get; set; }
        public required long CreatorId { get; set; }
        public required string Title { get; set; }
        public DateTime? FirstResponseDate { get; set; }
        public required string Status { get; set; }
        public DateTime? CloseDate { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
