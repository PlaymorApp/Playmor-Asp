using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Playmor_Asp.Application.DTOs;
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
    public IActionResult Register([FromBody] UserRegisterDTO userRegisterDTO)
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

    [HttpPost("auth/login")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult Login([FromBody] UserLoginDTO userLoginDTO)
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

    [HttpGet("auth/logout")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult Logout()
    {
        Response.Cookies.Delete("authToken");
        if (!ModelState.IsValid) { return BadRequest(ModelState); }
        return Ok();
    }
}
