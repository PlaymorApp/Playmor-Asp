using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Playmor_Asp.Application.DTOs;
using Playmor_Asp.Application.Interfaces;
using System.Security.Claims;

namespace Playmor_Asp.Presentation.Controllers;

[Route("api")]
[ApiController]
public class UserController : Controller
{
    private readonly IUserService _userService;
    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("users/profile")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDTO))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Authorize]
    public IActionResult GetUserProfile()
    {
        // Retrieve the user ID from the claims
        var uidClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (uidClaim == null || !int.TryParse(uidClaim, out var uid))
        {
            return BadRequest("User ID claim is missing or invalid.");
        }

        var user = _userService.GetUserById(uid);
        if (user == null)
        {
            return NotFound();
        }

        if (!ModelState.IsValid) { return BadRequest(ModelState); }
        return Ok(user);
    }

    [HttpGet("users/{userId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDTO))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Authorize]
    public IActionResult GetUser([FromRoute] int userId)
    {
        var user = _userService.GetUserById(userId);
        if (user == null)
        {
            return NotFound();
        }
        if (!ModelState.IsValid) { return BadRequest(ModelState); }
        return Ok(user);
    }
}
