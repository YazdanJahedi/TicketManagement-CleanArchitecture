using Application.DTOs;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.TicketFeatures.GetTicket
{
    public class GetTicketMapper : Profile
    {
        public GetTicketMapper() 
        {

             CreateMap<Ticket, GetTicketResponse>()
                .ForMember(dest => dest.Messages, opt => opt.MapFrom(src =>
                    src.Messages!.Select(m => new GetMessageDetailsDto
                    {
                        Text = m.Text,
                        Creator = new GetUserInformationDto
                        {
                            Name = m.Creator!.Name,
                            PhoneNumber = m.Creator.PhoneNumber,
                            Email = m.Creator.Email,
                            Role = m.Creator.Role
                        },
                        CreationDate = m.CreationDate
                    })))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status));
        }
    }
}
