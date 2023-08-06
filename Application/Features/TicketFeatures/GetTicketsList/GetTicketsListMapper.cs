using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.TicketFeatures.GetTicketsList
{
    public class GetTicketsListMapper : Profile
    {
        public GetTicketsListMapper()
        {
            CreateMap<Ticket, GetTicketsListResponse>();
        }
    }
}
