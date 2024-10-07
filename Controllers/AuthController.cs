using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Playmor_Asp.DTOs;
using Playmor_Asp.Interfaces;

namespace Playmor_Asp.Controllers;

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
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,      // This ensures the cookie is not accessible via JavaScript
                Secure = true,        // Only send the cookie over HTTPS (set to false if in development with HTTP)
                SameSite = SameSiteMode.None,  // Controls cross-site cookie behavior
                Expires = DateTime.Now.AddDays(1)
            };
            Response.Cookies.Append("authToken", jwt, cookieOptions);
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            return Ok();
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Exception occured in Register AuthController: {ex}");
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
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,      // This ensures the cookie is not accessible via JavaScript
                Secure = true,        // Only send the cookie over HTTPS (set to false if in development with HTTP)
                SameSite = SameSiteMode.None,  // Controls cross-site cookie behavior
                Expires = DateTime.Now.AddDays(1)
            };
            Response.Cookies.Append("authToken", jwt, cookieOptions);
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            return Ok();
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Exception occured in Login AuthController: {ex}");
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpGet("auth/logout")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult Logout()
    {
        try
        {
            Response.Cookies.Delete("authToken");
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            return Ok();
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Exception occured in Logout AuthController: {ex}");
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
}
