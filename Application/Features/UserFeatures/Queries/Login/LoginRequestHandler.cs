using Application.DTOs.UserDtos;
using Application.Repository;
using BCrypt.Net;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Web.Helpers;
using System.Web.Mvc;

namespace Application.Features.UserFeatures.Queries.Login
{
    public class LoginRequestQuery : IRequestHandler<LoginRequest, LoginResponse>
    {
        private readonly IUsersRepository _usersRepository;

        public LoginRequestQuery(IUsersRepository usersRepository) 
        {
            _usersRepository = usersRepository;
        }

        public async Task<LoginResponse> Handle(LoginRequest request, CancellationToken cancellationToken)
        {
            // Check validation
            if (!LoginRequestValidator.IsValid(request))
            {
                throw new Exception("validation error: Email or Password can not be empty");
            }

            // Find user by email
            var user = await _usersRepository.FindByEmailAsync(request.Email);
            if (user == null) throw new Exception("something"); // user or pass not correct 

            var isPasswordVerified = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);
            if (!isPasswordVerified) throw new Exception("something");  // user or pass not correct 

            // create token
            var token = CreateJwtToken.CreateToken(user);

            // create login response 
            var response = new LoginResponse
            {
                Email = user.Email,
                Role = user.Role,
                Token = token,
            };

            return response;
        }
    }
}
