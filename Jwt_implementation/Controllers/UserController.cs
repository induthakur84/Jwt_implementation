using Jwt_implementation.Dto;
using Jwt_implementation.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace Jwt_implementation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {


        //here we can implment dependency injection for user service
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterDto userRegisterDto)
        {
            //call the register method from user service
            var result = await _userService.Register(userRegisterDto);
            if (result == null)
            {
                return BadRequest("User registration failed");
            }
            //return the result
            return Ok(result);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            try
            {
                var result = await _userService.Login(loginDto);
                return Ok(new
                {
                    message="Login successful",
                    token=result.Token,
                    user=result.User
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    message = "Login failed",
                    error = ex.Message
                });
            }
        }
    }
}
