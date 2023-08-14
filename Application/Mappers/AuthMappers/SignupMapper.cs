using Application.Dtos.UserDtos;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappers.AuthMappers
{
    public class SignupMapper : Profile
    {
        public SignupMapper()
        {
            CreateMap<User, SignupResponse>();

            CreateMap<SignupRequest, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => BCrypt.Net.BCrypt.HashPassword(src.Password)))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => "User"))
                .ForMember(dest => dest.CreationDate, opt => opt.MapFrom(src => DateTime.Now));
        }
    }
}
