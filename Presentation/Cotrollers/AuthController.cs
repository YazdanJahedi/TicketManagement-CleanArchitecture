using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Application.DTOs.UserDtos;
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

        [HttpPost("signup")]
        public async Task<ActionResult<SignupResponse>> Signup(SignupRequest req)
        {
            try
            {
                var response = await _mediator.Send(req);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new ExceptionDto(ex.GetType().Name, ex.Message));
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponse>> Login(LoginRequest req)
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
