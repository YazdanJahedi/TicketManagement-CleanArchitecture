using Application.Common.DTOs;
using Application.Dtos.TicketDtos;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mappers
{
    public class GetTicketsListMapper : Profile
    {
        public GetTicketsListMapper()
        {
            CreateMap<Ticket, GetTicketsListResponse>()
                .ForMember(dest => dest.Creator, opt => opt.MapFrom(src =>
                    new GetUserInformationDto
                    {
                        Name = src.Creator!.Name,
                        Email = src.Creator.Email,
                        Role = src.Creator.Role,
                        PhoneNumber = src.Creator.PhoneNumber,
                    }
                ));
        }
    }
}
