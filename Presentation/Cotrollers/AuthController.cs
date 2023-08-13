using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Application.Common.DTOs;
using Application.Dtos.UserDtos;
using Application.Interfaces.Service;

namespace Presentation.Controllers
{
    [Route("api/")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
     
        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("signup")]
        public async Task<ActionResult<SignupResponse>> Signup([FromForm] SignupRequest req)
        {
            try
            {
                var response = await _userService.Signup(req);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new ExceptionDto(ex.GetType().Name, ex.Message));
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponse>> Login([FromForm] LoginRequest req)
        {
            try
            {
                var response = await _userService.Login(req);
                return Ok(response);
            } 
            catch (Exception ex)
            {  
                return BadRequest(new ExceptionDto(ex.GetType().Name , ex.Message));               
            }
            
        }

    }
}
