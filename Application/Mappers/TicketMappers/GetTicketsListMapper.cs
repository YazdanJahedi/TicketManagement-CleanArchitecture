using Application.Dtos.TicketDtos;
using Application.Dtos.UserDtos;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappers.TicketMappers
{
    public class GetTicketsListMapper : Profile
    {
        public GetTicketsListMapper()
        {
            CreateMap<Ticket, GetTicketsListResponse>()
                .ForMember(dest => dest.Creator, opt => opt.MapFrom(src =>
                    new UserDetailsDto
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
