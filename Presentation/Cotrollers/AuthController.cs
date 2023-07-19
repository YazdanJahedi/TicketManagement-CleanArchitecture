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

            _usersRepository.CheckNull();

            // validation check
            CreateUserValidator validator = new();
            var validatorResult = validator.Validate(req);
            if (!validatorResult.IsValid)
            {
                return BadRequest(validatorResult.Errors);
            }

            // check if email is used
            var foundUser = _usersRepository.FindByEmail(req.Email); 
            if (foundUser != null)
            {
                return BadRequest("this Email is used before");
            }

            // creating new user
            string passHash = BCrypt.Net.BCrypt.HashPassword(req.Password);

            var user = new User
            {
                Name = req.Name,
                Email = req.Email,
                Role = "User",
                PhoneNumber = req.PhoneNumber,
                PasswordHash = passHash,
                CreationDate = DateTime.Now,
            };

            _usersRepository.Add(user);
            return Ok(user);
        }


        [HttpPost("login")]
        public ActionResult<User> Login(LoginRequestDto req)
        {
            _usersRepository.CheckNull();

            // check validation
            LoginRequestValidator validator = new();
            var validatorResult = validator.Validate(req);
            if (!validatorResult.IsValid)
            {
                return BadRequest(validatorResult.Errors);
            }

            // find user by email
            var user = _usersRepository.FindByEmail(req.Email);
            if (user == null)
            {
                return BadRequest("Email not found");
            }

            // check user's password 
            if (!BCrypt.Net.BCrypt.Verify(req.Password, user.PasswordHash))
                return BadRequest("password not correct");

            // create token
            var section = _conf.GetSection("AppSettings:Token");
            var token = CreateJwtToken.CreateToken(user, section);

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
