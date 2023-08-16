using Application.Common.Exceptions;
using Application.Dtos.UserDtos;
using Application.Interfaces.Repository;
using Application.Interfaces.Service;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Application.Services
{
    public class AuthService : IAuthService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public AuthService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<LoginResponse> Login(LoginRequest request)
        {
            // Find by email and check user 
            var user = await _unitOfWork.UsersRepository.GetByConditionAsync(e => e.Email == request.Email);
            if (user == null) throw new NotFoundException("username or password is not correct");

            // Check password
            var isPasswordVerified = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);
            if (!isPasswordVerified) throw new NotFoundException("username or password is not correct");

            // create login response 
            var response = _mapper.Map<LoginResponse>(user);
            return response;
        }

        public async Task<SignupResponse> Signup(SignupRequest request)
        {
            // Check if email is used
            var foundUser = await _unitOfWork.UsersRepository.GetByConditionAsync(e => e.Email == request.Email);
            if (foundUser != null) throw new Exception("this Email is used before");

            // Creating new user
            var user = _mapper.Map<User>(request);

            // Add user to DB
            await _unitOfWork.UsersRepository.AddAsync(user);
            await _unitOfWork.SaveAsync();

            var response = _mapper.Map<SignupResponse>(user);
            return response;
        }

        public UserClaimsDto GetClaims()
        {
            var id = _httpContextAccessor.HttpContext?.User
                .Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var role = _httpContextAccessor.HttpContext?.User
                .Claims.First(x => x.Type == ClaimTypes.Role).Value;
            if (role == null || id == null) throw new NotFoundException("user not authorized");

            var response = new UserClaimsDto
            {
                Id = Convert.ToInt64(id),
                Role = role,
            };

            return response;
        }
    }
}
