using Application.Dtos.UserDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Service
{
    public interface IUserService
    {
        public Task<SignupResponse> Signup(SignupRequest request);
        public Task<LoginResponse> Login(LoginRequest request); 
    }
}
