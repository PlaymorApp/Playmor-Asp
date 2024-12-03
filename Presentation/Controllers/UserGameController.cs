using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Playmor_Asp.Application.Common.Errors;
using Playmor_Asp.Application.DTOs.UserGame;
using Playmor_Asp.Application.Interfaces;
using Playmor_Asp.Domain.Models;
using Playmor_Asp.Helpers;

namespace Playmor_Asp.Presentation.Controllers;
[Route("api")]
[ApiController]
public class UserGameController : Controller
{
    private readonly IUserGameService _userGameService;
    public UserGameController(IUserGameService userGameService)
    {
        _userGameService = userGameService;
    }

    [HttpGet("userGames/${id}")]
    [ProducesResponseType(200, Type = typeof(UserGame))]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetUserGameByIdAsync([FromRoute] int id)
    {
        if (!ModelState.IsValid) { return BadRequest(ModelState); }

        if (User.GetUserId() is not int uid)
        {
            return Unauthorized("Unauthorized to access resource");
        }

        var serviceResult = await _userGameService.GetUserGameByIdAsync(id, uid);

        if (serviceResult.IsValid)
        {
            return Ok(serviceResult.Data);
        }

        if (!serviceResult.IsValid && serviceResult.Errors.Any(e => e is ValidationError))
        {
            return BadRequest(serviceResult.Errors);
        }

        return StatusCode(StatusCodes.Status500InternalServerError, serviceResult.Errors);
    }

    [HttpGet("userGames/byUser/${userId}")]
    [ProducesResponseType(200, Type = typeof(ICollection<UserGame>))]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> GetUserGamesByUserIdAsync([FromRoute] int userId)
    {
        if (!ModelState.IsValid) { return BadRequest(ModelState); }

        if (User.GetUserId() is not int uid)
        {
            return Unauthorized("Unauthorized to access resource");
        }

        var serviceResult = await _userGameService.GetUserGamesByUserIdAsync(userId, uid);

        if (serviceResult.IsValid)
        {
            return Ok(serviceResult.Data);
        }

        if (!serviceResult.IsValid && serviceResult.Errors.Any(e => e is ValidationError))
        {
            return BadRequest(serviceResult.Errors);
        }

        return StatusCode(StatusCodes.Status500InternalServerError, serviceResult.Errors);
    }

    [HttpPost("userGames")]
    [ProducesResponseType(200, Type = typeof(UserGameDTO))]
    [ProducesResponseType(400)]
    [ProducesResponseType(401)]
    [ProducesResponseType(500)]
    [Authorize]
    public async Task<IActionResult> CreateUserGameAsync([FromBody] UserGamePostDTO userGame)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (User.GetUserId() is not int uid)
        {
            return Unauthorized("Unauthorized to access resource");
        }

        var serviceResult = await _userGameService.CreateUserGameAsync(userGame);
        if (!serviceResult.IsValid && serviceResult.Errors.Any(e => e is ValidationError))
        {
            return BadRequest(serviceResult.Errors);
        }


        return Ok(serviceResult.Data);
    }
}
