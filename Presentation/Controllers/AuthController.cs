using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Playmor_Asp.Application.Common.Errors;
using Playmor_Asp.Application.DTOs.User;
using Playmor_Asp.Application.Interfaces;

namespace Playmor_Asp.Presentation.Controllers;

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
    public IActionResult Register([FromBody] UserPostDTO userRegisterDTO)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var serviceResult = _authService.Register(userRegisterDTO);

        if (!serviceResult.IsValid)
        {
            if (serviceResult.Errors.Any(e => e is ValidationError))
            {
                return BadRequest("Invalid data passed.");
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Unexpected server error");
            }
        }

        string jwt = serviceResult.Data;

        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,      // This ensures the cookie is not accessible via JavaScript
            Secure = true,        // Only send the cookie over HTTPS (set to false if in development with HTTP)
            SameSite = SameSiteMode.None,  // Controls cross-site cookie behavior
            Expires = DateTime.Now.AddDays(1)
        };

        Response.Cookies.Append("authToken", jwt, cookieOptions);

        return Ok(new { message = "Registration successful." });
    }

    [HttpPost("auth/login")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult Login([FromBody] UserAuthDTO userLoginDTO)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var serviceResult = _authService.Login(userLoginDTO);

        if (!serviceResult.IsValid)
        {
            if (serviceResult.Errors.Any(e => e is ValidationError))
            {
                return BadRequest("Invalid data passed.");
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Unexpected server error");
            }
        }

        string jwt = serviceResult.Data;

        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,      // This ensures the cookie is not accessible via JavaScript
            Secure = true,        // Only send the cookie over HTTPS (set to false if in development with HTTP)
            SameSite = SameSiteMode.None,  // Controls cross-site cookie behavior
            Expires = DateTime.Now.AddDays(1)
        };
        Response.Cookies.Append("authToken", jwt, cookieOptions);

        return Ok(new { message = "Login successful." });
    }

    [HttpGet("auth/logout")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult Logout()
    {
        Response.Cookies.Delete("authToken");

        return Ok(new { message = "Logout successful." });
    }
}
