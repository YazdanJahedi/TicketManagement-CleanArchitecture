using Application.Dtos.UserDtos;
using Application.Services;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mappers
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
