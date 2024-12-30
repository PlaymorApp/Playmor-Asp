using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Playmor_Asp.Application.Common.Errors;
using Playmor_Asp.Application.Common.Types;
using Playmor_Asp.Application.DTOs.UserGame;
using Playmor_Asp.Application.Interfaces;
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

    [HttpGet("userGames/{id}")]
    [ProducesResponseType(200, Type = typeof(UserGameDTO))]
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

    [HttpGet("userGames/byUser/{userId}")]
    [ProducesResponseType(200, Type = typeof(ICollection<UserGameDTO>))]
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

    [HttpDelete("userGames/{userGameId}")]
    [ProducesResponseType(200, Type = typeof(bool))]
    [ProducesResponseType(400)]
    [ProducesResponseType(401)]
    [ProducesResponseType(500)]
    [Authorize]
    public async Task<IActionResult> DeleteUserGameAsync([FromRoute] int userGameId)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (User.GetUserId() is not int uid)
        {
            return Unauthorized("Unauthorized to access endpoint");
        }

        var serviceResult = await _userGameService.DeleteUserGameAsync(userGameId, uid);

        if (!serviceResult.IsValid)
        {
            if (serviceResult.Errors.Any(e => e is ValidationError))
            {
                return BadRequest(serviceResult.Errors);
            }

            if (serviceResult.Errors.Any(e => e is NotFoundError))
            {
                return NotFound();
            }

            if (serviceResult.Errors.Any(e => e is UnauthorizedError))
            {
                return Unauthorized("Unauthorized to access endpoint");
            }

            if (serviceResult.Errors.Any(e => e is UnexpectedError))
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Unexpected server error");
            }
        }


        return Ok(serviceResult.Data);
    }

    [HttpGet("userGames/statistics/{userId}")]
    [ProducesResponseType(200, Type = typeof(UserGameStatistics))]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> GetUserGamesStatisticsAsync([FromRoute] int userId)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var serviceResult = await _userGameService.GetUserGamesStatisticsAsync(userId);

        if (!serviceResult.IsValid)
        {
            if (serviceResult.Errors.Any(e => e is NotFoundError))
            {
                return NotFound();
            }

            if (serviceResult.Errors.Any(e => e is UnexpectedError))
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Unexpected server error");
            }
        }

        return Ok(serviceResult.Data);
    }

    [HttpGet("userGames/{userId}/tracks/{gameId}")]
    [ProducesResponseType(200, Type = typeof(UserGameDTO))]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> GetUserGameTrackedByUserId([FromRoute] int userId, [FromRoute] int gameId)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var serviceResult = await _userGameService.GetUserGamesByUserIdAsync(userId);

        if (!serviceResult.IsValid)
        {
            if (serviceResult.Errors.Any(e => e is UnexpectedError))
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Unexpected server error");
            }
        }

        var matching = serviceResult.Data.Select(ug => ug).Where(ug => ug.GameId == gameId && ug.UserId == userId).First();

        return Ok(matching);
    }
}
