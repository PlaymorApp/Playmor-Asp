using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Playmor_Asp.Application.DTOs.User;
using Playmor_Asp.Application.Interfaces;
using Playmor_Asp.Helpers;

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
        if (User.GetUserId() is not int userId)
        {
            return Unauthorized("Unauthorized to access resource");
        }

        var user = _userService.GetUserById(userId);
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
