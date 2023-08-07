using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Application.DTOs;
using Application.Features.UserFeatures.Signup;
using Application.Features.UserFeatures.Login;

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
        public async Task<ActionResult<SignupResponse>> Signup([FromForm] SignupRequest req)
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
        public async Task<ActionResult<LoginResponse>> Login([FromForm] LoginRequest req)
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
