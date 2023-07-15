using Application.Features.CreateUser;
using Application.Features.LoginUser;
using Domain.Entities;
using FluentValidation;
using Infrastructure.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using Application.Features;
using Infrastructure.Queries;

namespace Presentation.Controllers
{
    [Route("api/")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _conf;

        public AuthController(IConfiguration conf, ApplicationDbContext context)
        {
            _conf = conf;
            _context = context;
        }

        [HttpPost("signup")]
        public async Task<ActionResult<User>> Signup(CreateUserRequest req)
        {

            if (_context.Users == null)
            {
                return NotFound();
            }


            CreateUserValidator validator = new();
            var validatorResult = validator.Validate(req);
            if (!validatorResult.IsValid)
            {
                return BadRequest(validatorResult.Errors);
            }

            if (!UserQueries.IsUserFound(_context, req.Email))
            {
                return BadRequest("this Email is used before");
            }

            string passHash = BCrypt.Net.BCrypt.HashPassword(req.Password);

            // first user is admin
            string role = "User";
            if (_context.Users.IsNullOrEmpty())
            {
                role = "Admin";
            }

            var user = new User
            {
                Name = req.Name,
                Email = req.Email,
                Role = role,
                PhoneNumber = req.PhoneNumber,
                PasswordHash = passHash,
                CreationTime = DateTime.Now,
            };


            _context.Users.Add(user);
            // _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return Ok(user);
        }


        [HttpPost("login")]
        public async Task<ActionResult<User>> Login(LoginRequest req)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }

            LoginRequestValidator validator = new();
            var validatorResult = validator.Validate(req);
            if (!validatorResult.IsValid)
            {
                return BadRequest(validatorResult.Errors);
            }

            if (!UserQueries.IsUserFound(_context,req.Email))
            {
                return BadRequest("Email not found");
            }

            var user = _context.Users.FirstOrDefault(e => e.Email == req.Email);

            if (user == null)
            {
                return NotFound();
            }

            if (!BCrypt.Net.BCrypt.Verify(req.Password, user.PasswordHash))
                return BadRequest("password not correct");


            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_conf.GetSection("AppSettings:Token").Value!));
            var token = CreateUserToken.CreateToken(user,  key);

            return Ok("bearer " + token);
        }

        [HttpGet("WhoAmI"), Authorize]
        public string WhoAmI()
        {
            var email = User.Claims.Where(x => x.Type == ClaimTypes.Email).FirstOrDefault()?.Value;
            var role = User.Claims.Where(x => x.Type == ClaimTypes.Role).FirstOrDefault()?.Value;
            return email + "   role: " + role;
        }

    }
}
