using Application.Features.CreateUser;
using Application.Features.LoginUser;
using Domain.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Application.Features;
using Application.Repository;
using Application.DTOs;
using Application.DTOs.LoginDtos;

namespace Presentation.Controllers
{
    [Route("api/")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IUsersRepository _usersRepository;
        private readonly IConfiguration _conf;

        public AuthController(IConfiguration conf, IUsersRepository usersRepository)
        {
            _conf = conf;
            _usersRepository = usersRepository;
        }

        [HttpPost("signup")]
        public ActionResult<User> Signup(CreateUserDto req)
        {
            // Validation check
            if (!CreateUserValidator.IsValid(req))
            {
                return BadRequest("validation error: Email must be in email format and password can not be empty");
            }

            // Check if email is used
            var foundUser = _usersRepository.FindByEmail(req.Email); 
            if (foundUser != null)
            {
                return BadRequest("this Email is used before");
            }

            // Creating new user
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(req.Password);
            var user = new User
            {
                Name = req.Name,
                Email = req.Email,
                Role = "User",
                PhoneNumber = req.PhoneNumber,
                PasswordHash = hashedPassword,
                CreationDate = DateTime.Now,
            };

            // Add new user to the data base
            _usersRepository.AddAsync(user);
            return Ok(user);
        }

        [HttpPost("login")]
        public ActionResult<User> Login(LoginRequestDto req)
        {
            // Check validation
            if (!LoginRequestValidator.IsValid(req))
            {
                return BadRequest("validation error: Email or Password can not be empty");
            }

            // Find user by email
            var user = _usersRepository.FindByEmail(req.Email);
            if (user == null)
            {
                return BadRequest("Email not found");
            }

            // Check user's password 
            if (!BCrypt.Net.BCrypt.Verify(req.Password, user.PasswordHash))
            {
                return BadRequest("Password not correct");
            }

            // create token
            var section = _conf.GetSection("AppSettings:Token");
            var token = CreateJwtToken.CreateToken(user, section);

            // create login response 
            var response = new LoginResponseDto
            {             
                Email = user.Email,
                Role = user.Role,
                Token = token,
            };

            return Ok(response);
        }

    }
}
