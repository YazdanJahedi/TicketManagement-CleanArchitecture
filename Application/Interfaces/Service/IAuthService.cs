using Application.Dtos.UserDtos;

namespace Application.Interfaces.Service
{
    public interface IAuthService
    {
        public Task<SignupResponse> Signup(SignupRequest request);
        public Task<LoginResponse> Login(LoginRequest request);
        public UserClaimsDto GetClaims();
    }
}
