using Application.Features.CreateUser;
using Application.Features.LoginUser;
using Domain.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using Application.Features;
using Application.Repository;
using Application.DTOs;

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

            if (_usersRepository.IsContextNull())
            {
                return NotFound();
            }

            // validation check
            CreateUserValidator validator = new();
            var validatorResult = validator.Validate(req);
            if (!validatorResult.IsValid)
            {
                return BadRequest(validatorResult.Errors);
            }

            // check if email is used
            if (_usersRepository.IsUserFoundByEmail(req.Email))
            {
                return BadRequest("this Email is used before");
            }

            // first signed up user is Admin
            string role = "User";
            if (_usersRepository.IsContextEmptyOrNull())
            {
                role = "Admin";
            }

            // creating new user
            string passHash = BCrypt.Net.BCrypt.HashPassword(req.Password);
            var user = new User
            {
                Name = req.Name,
                Email = req.Email,
                Role = role,
                PhoneNumber = req.PhoneNumber,
                PasswordHash = passHash,
                CreationTime = DateTime.Now,
            };

            _usersRepository.AddUserAsync(user);
            return Ok(user);
        }


        [HttpPost("login")]
        public ActionResult<User> Login(LoginRequestDto req)
        {
            if (_usersRepository.IsContextNull())
            {
                return NotFound();
            }

            // check validation
            LoginRequestValidator validator = new();
            var validatorResult = validator.Validate(req);
            if (!validatorResult.IsValid)
            {
                return BadRequest(validatorResult.Errors);
            }

            // check for email in database
            if (!_usersRepository.IsUserFoundByEmail(req.Email))
            {
                return BadRequest("Email not found");
            }

            // find user in database
            var user = _usersRepository.FindUserByEmail(req.Email);

            if (user == null)
            {
                return NotFound();
            }

            // check user's password 
            if (!BCrypt.Net.BCrypt.Verify(req.Password, user.PasswordHash))
                return BadRequest("password not correct");

            // create token
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_conf.GetSection("AppSettings:Token").Value!));
            var token = CreateUserToken.CreateToken(user,  key);

            return Ok("bearer " + token);
        }

        [HttpGet("WhoAmI"), Authorize]
        public string WhoAmI()
        {
            var email = User.Claims.Where(x => x.Type == ClaimTypes.Email).FirstOrDefault()?.Value;
            var role = User.Claims.Where(x => x.Type == ClaimTypes.Role).FirstOrDefault()?.Value;
            return $"email: {email}   role: {role}";
        }

    }
}
