using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Application.Features;
using Application.Repository;
using Application.DTOs.UserDtos;
using Application.Features.UserFeatures.Queries.Login;
using MediatR;
using Application.DTOs;

namespace Presentation.Controllers
{
    [Route("api/")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IMediator _mediator;
        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /*[HttpPost("signup")]
        public ActionResult<User> Signup(SignupRequest req)
        {
            // Validation check
            if (!SignupRequestValidator.IsValid(req))
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
        }*/

        [HttpPost("login")]
        public async Task<ActionResult<User>> Login(LoginRequest req)
        {
            try
            {
                var response = await _mediator.Send(req);
                return Ok(response);
            } 
            catch (Exception ex)
            {  
                return BadRequest(new ExceptionDto(ex.GetType().Name , ex.Message));               
            }
            
        }

    }
}
