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

        [HttpPost("signup")]
        public ActionResult<User> Signup(SignupRequest req)
        {
            try
            {
                var response = _mediator.Send(req);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new ExceptionDto(ex.GetType().Name, ex.Message));
            }
        }

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
