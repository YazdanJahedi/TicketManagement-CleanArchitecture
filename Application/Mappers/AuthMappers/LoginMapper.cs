using Application.Dtos.UserDtos;
using Application.Services;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappers.AuthMappers
{
    public class LoginMapper : Profile
    {
        public LoginMapper()
        {
            CreateMap<User, LoginResponse>()
                .ForMember(dest => dest.Token, opt => opt.MapFrom(src => CreateJwtToken.CreateToken(src)));
        }
    }
}
