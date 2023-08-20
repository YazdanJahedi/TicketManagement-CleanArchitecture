using Microsoft.AspNetCore.Mvc;
using Application.Dtos.UserDtos;
using Application.Interfaces.Service;

namespace Presentation.Controllers
{
    [Route("api/")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUnitOfSevice _unitOfSevice;
     
        public AuthController(IUnitOfSevice unitOfService)
        {
            _unitOfSevice = unitOfService;
        }

        [HttpPost]
        [Route("signup")]
        public async Task<ActionResult<SignupResponse>> Signup([FromForm] SignupRequest req)
        {
            try
            {
                var response = await _unitOfSevice.AuthService.Signup(req);
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
                var response = await _unitOfSevice.AuthService.Login(req);
                return Ok(response);
            } 
            catch (Exception ex)
            {  
                return BadRequest(ex);               
            }
            
        }

    }
}
