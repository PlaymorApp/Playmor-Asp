using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Playmor_Asp.Application.Common.Errors;
using Playmor_Asp.Application.DTOs;
using Playmor_Asp.Application.Interfaces;
using Playmor_Asp.Domain.Models;
using System.Security.Claims;

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

        var serviceResult = await _userGameService.GetUserGameByIdAsync(id);

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

        var serviceResult = await _userGameService.GetUserGamesByUserIdAsync(userId);

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
    [Authorize]
    public async Task<IActionResult> CreateUserGameAsync([FromBody] UserGamePostDTO userGame)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var uidClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (uidClaim == null || !int.TryParse(uidClaim, out var uid))
        {
            return BadRequest("User ID claim is missing or invalid.");
        }

        var serviceResult = await _userGameService.CreateUserGameAsync(userGame);
        if (!serviceResult.IsValid && serviceResult.Errors.Any(e => e is ValidationError))
        {
            return BadRequest(serviceResult.Errors);
        }


        return Ok(serviceResult.Data);
    }
}
