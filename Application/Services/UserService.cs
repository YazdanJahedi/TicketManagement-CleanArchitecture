using Application.Common.Exceptions;
using Application.Dtos.UserDtos;
using Application.Interfaces.Repository;
using Application.Interfaces.Service;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{


    public class UserService : IUserService
    {

        private readonly IUsersRepository _usersRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public UserService(IUsersRepository usersRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _usersRepository = usersRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<LoginResponse> Login(LoginRequest request)
        {
            // Find user by email
            var user = await _usersRepository.GetAsync(e => e.Email == request.Email);
            if (user == null) throw new NotFoundException("username or password is not correct");

            var isPasswordVerified = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);
            if (!isPasswordVerified) throw new NotFoundException("username or password is not correct");

            // create login response 
            var response = _mapper.Map<LoginResponse>(user);

            return response;
        }

        public async Task<SignupResponse> Signup(SignupRequest request)
        {
            // Check if email is used
            var foundUser = await _usersRepository.GetAsync(e => e.Email == request.Email);
            if (foundUser != null) throw new Exception("this Email is used before");

            // Creating new user
            var user = _mapper.Map<User>(request);

            // Add new user to the data base
            await _usersRepository.AddAsync(user);

            var response = _mapper.Map<SignupResponse>(user);
            return response;
        }

        public UserClaimsDto GetClaims()
        {
            var idString = _httpContextAccessor.HttpContext?.User.
                Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var role = _httpContextAccessor.HttpContext?.User.
                Claims.First(x => x.Type == ClaimTypes.Role).Value;
            if (role == null) throw new NotFoundException("user not found");

            var userId = Convert.ToInt64(idString);

            var response = new UserClaimsDto
            {
                Id = userId,
                Role = role,
            };

            return response;
        }
    }
}
