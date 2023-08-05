using AutoMapper;
using Domain.Entities;

namespace Application.Features.UserFeatures.Signup
{
    public class SignupMapper : Profile
    {
        public SignupMapper()
        {
            CreateMap<User, SignupResponse>();
        }
    }
}
