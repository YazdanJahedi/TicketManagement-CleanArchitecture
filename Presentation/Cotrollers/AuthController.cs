using Microsoft.AspNetCore.Mvc;
using Application.Dtos.UserDtos;
using Application.Interfaces.Service;

namespace Presentation.Controllers
{
    [Route("api/")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
     
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        [Route("signup")]
        public async Task<ActionResult<SignupResponse>> Signup([FromForm] SignupRequest req)
        {
            try
            {
                var response = await _authService.Signup(req);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<LoginResponse>> Login([FromForm] LoginRequest req)
        {
            try
            {
                var response = await _authService.Login(req);
                return Ok(response);
            } 
            catch (Exception ex)
            {  
                return BadRequest(ex);               
            }
            
        }

    }
}
