using Microsoft.AspNetCore.Mvc;
using Playmor_Asp.DTOs;
using Playmor_Asp.Interfaces;
using Playmor_Asp.Models;
using Playmor_Asp.Services;

namespace Playmor_Asp.Controllers
{
    [Route("api")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("auth/register")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Register([FromBody] UserRegisterDTO userRegisterDTO)
        {
            try
            {
                string jwt = _authService.Register(userRegisterDTO);
                if (!ModelState.IsValid) { return BadRequest(ModelState); }
                return Ok(jwt);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Exception occured in PostUser AuthController: {ex}");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("auth/login")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Login([FromBody] UserLoginDTO userLoginDTO)
        {
            try
            {
                string jwt = _authService.Login(userLoginDTO);
                if (!ModelState.IsValid) { return BadRequest(ModelState); }
                return Ok(jwt);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Exception occured in PostUser AuthController: {ex}");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
