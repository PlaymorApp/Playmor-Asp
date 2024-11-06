using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Playmor_Asp.Application.Common.Errors;
using Playmor_Asp.Application.Common.Filters;
using Playmor_Asp.Application.Common.Types;
using Playmor_Asp.Application.DTOs;
using Playmor_Asp.Application.Interfaces;
using Playmor_Asp.Domain.Enums;
using Playmor_Asp.Domain.Models;

namespace Playmor_Asp.Presentation.Controllers;

[Route("api")]
[ApiController]
public class GameController : Controller
{
    private readonly IGameService _gameService;
    public GameController(IGameService gameService)
    {
        _gameService = gameService;
    }

    [HttpGet("games")]
    [ProducesResponseType(200, Type = typeof(ICollection<Game>))]
    [ProducesResponseType(500)]
    public async Task<IActionResult> GetGamesAsync()
    {
        var serviceResult = await _gameService.GetGamesAsync();

        if (serviceResult.IsValid)
        {
            return Ok(serviceResult.Data);
        }

        return StatusCode(StatusCodes.Status500InternalServerError, serviceResult.Errors);
    }

    [HttpGet("games/paginated")]
    [ProducesResponseType(200, Type = typeof(GamePagination))]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> GetPaginatedGamesAsync([FromQuery] int pageNumber, [FromQuery] int pageSize, [FromQuery] SortByOrder? sortBy, [FromQuery] GameFilter? gameFilter)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var serviceResult = await _gameService.GetPaginatedGamesAsync(pageNumber, pageSize, sortBy, gameFilter);

        if (serviceResult.IsValid)
        {
            return Ok(serviceResult.Data);
        }

        return StatusCode(StatusCodes.Status500InternalServerError, serviceResult.Errors);
    }

    [HttpGet("games/added")]
    [ProducesResponseType(200, Type = typeof(ICollection<Game>))]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> GetGamesByAddedDateAsync([FromQuery] string sort)
    {
        var serviceResult = await _gameService.GetGamesByAddedDateAsync(sort);

        if (!ModelState.IsValid) { return BadRequest(ModelState); }

        if (serviceResult.IsValid)
        {
            return Ok(serviceResult.Data);
        }

        return StatusCode(StatusCodes.Status500InternalServerError, serviceResult.Errors);
    }

    [HttpGet("games/released")]
    [ProducesResponseType(200, Type = typeof(ICollection<Game>))]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> GetGamesByReleaseDateAsync([FromQuery] string sort)
    {
        var serviceResult = await _gameService.GetGamesByReleaseDateAsync(sort);

        if (!ModelState.IsValid) { return BadRequest(ModelState); }

        if (serviceResult.IsValid)
        {
            return Ok(serviceResult.Data);
        }

        return StatusCode(StatusCodes.Status500InternalServerError, serviceResult.Errors);
    }

    [HttpGet("games/{gameId}")]
    [ProducesResponseType(200, Type = typeof(Game))]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> GetGameAsync(int gameId)
    {
        var serviceResult = await _gameService.GetGameAsync(gameId);

        if (!ModelState.IsValid) { return BadRequest(ModelState); }

        if (!serviceResult.IsValid)
        {
            if (serviceResult.Errors.Any(e => e is NotFoundError))
            {
                return NotFound();
            }
        }

        if (serviceResult.IsValid)
        {
            return Ok(serviceResult.Data);
        }

        return StatusCode(StatusCodes.Status500InternalServerError, serviceResult.Errors);
    }

    [HttpGet("games/title/{title}")]
    [ProducesResponseType(200, Type = typeof(Game))]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> GetGamesByTitleAsync(string title)
    {
        var serviceResult = await _gameService.GetGamesByTitleAsync(title);

        if (!ModelState.IsValid) { return BadRequest(ModelState); }

        if (serviceResult.IsValid)
        {
            return Ok(serviceResult.Data);
        }

        return StatusCode(StatusCodes.Status500InternalServerError, serviceResult.Errors);
    }

    [HttpGet("games/genres")]
    [ProducesResponseType(200, Type = typeof(Game))]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> GetGamesByGenresAsync([FromQuery] ICollection<string> genres)
    {
        var serviceResult = await _gameService.GetGamesByGenresAsync(genres);

        if (!ModelState.IsValid) { return BadRequest(ModelState); }

        if (serviceResult.IsValid)
        {
            return Ok(serviceResult.Data);
        }

        return StatusCode(StatusCodes.Status500InternalServerError, serviceResult.Errors);
    }

    [HttpGet("games/modes")]
    [ProducesResponseType(200, Type = typeof(Game))]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> GetGamesByModesAsync([FromQuery] ICollection<string> modes)
    {
        var serviceResult = await _gameService.GetGamesByModesAsync(modes);

        if (!ModelState.IsValid) { return BadRequest(ModelState); }

        if (serviceResult.IsValid)
        {
            return Ok(serviceResult.Data);
        }

        return StatusCode(StatusCodes.Status500InternalServerError, serviceResult.Errors);
    }

    [HttpGet("games/keyword/{keyword}")]
    [ProducesResponseType(200, Type = typeof(Game))]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> GetGamesByKeywordAsync(string keyword)
    {
        var serviceResult = await _gameService.GetGamesByKeywordAsync(keyword);

        if (!ModelState.IsValid) { return BadRequest(ModelState); }

        if (serviceResult.IsValid)
        {
            return Ok(serviceResult.Data);
        }

        return StatusCode(StatusCodes.Status500InternalServerError, serviceResult.Errors);
    }

    [HttpPost("games")]
    [ProducesResponseType(200, Type = typeof(bool))]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateGame([FromBody] GameDTO game)
    {
        var serviceResult = await _gameService.CreateGameAsync(game);

        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        if (!serviceResult.IsValid)
        {
            if (serviceResult.Errors.Any(e => e is NotFoundError))
            {
                return NotFound();
            }

            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        return Ok();
    }

    [HttpPut("games/{gameId}")]
    [ProducesResponseType(200, Type = typeof(bool))]
    [ProducesResponseType(400)]
    [ProducesResponseType(401)]
    [ProducesResponseType(500)]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update([FromRoute] int gameId, [FromBody] GameDTO game)
    {
        var serviceResult = await _gameService.UpdateGameAsync(gameId, game);

        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        if (!serviceResult.IsValid)
        {
            if (serviceResult.Errors.Any(e => e is NotFoundError))
            {
                return NotFound();
            }

            if (serviceResult.Errors.Any(e => e is ValidationError))
            {
                return BadRequest();
            }

            return StatusCode(StatusCodes.Status500InternalServerError, "Unexpected server error");
        }

        return Ok();
    }

    [HttpDelete("games/{gameId}")]
    [ProducesResponseType(200, Type = typeof(bool))]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteGame([FromRoute] int gameId)
    {
        var serviceResult = await _gameService.DeleteGameAsync(gameId);

        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        if (!serviceResult.IsValid)
        {
            if (serviceResult.Errors.Any(e => e is NotFoundError))
            {
                return NotFound();
            }

            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        return Ok();
    }

    [HttpGet("games/available/modes")]
    [ProducesResponseType(200, Type = typeof(bool))]
    [ProducesResponseType(500)]
    public async Task<IActionResult> GetAvailableModes()
    {
        var serviceResult = await _gameService.GetGameModesAsync();

        if (!serviceResult.IsValid)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        return Ok(serviceResult.Data);
    }

    [HttpGet("games/available/genres")]
    [ProducesResponseType(200, Type = typeof(bool))]
    [ProducesResponseType(500)]
    public async Task<IActionResult> GetAvailableGenres()
    {
        var serviceResult = await _gameService.GetGameGenresAsync();

        if (!serviceResult.IsValid)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        return Ok(serviceResult.Data);
    }

    [HttpGet("games/available/platforms")]
    [ProducesResponseType(200, Type = typeof(bool))]
    [ProducesResponseType(500)]
    public async Task<IActionResult> GetAvailablePlatforms()
    {
        var serviceResult = await _gameService.GetGamePlatformsAsync();

        if (!serviceResult.IsValid)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        return Ok(serviceResult.Data);
    }

    [HttpGet("games/available/developers")]
    [ProducesResponseType(200, Type = typeof(bool))]
    [ProducesResponseType(500)]
    public async Task<IActionResult> GetAvailableDevelopers()
    {
        var serviceResult = await _gameService.GetGameDevelopersAsync();

        if (!serviceResult.IsValid)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        return Ok(serviceResult.Data);
    }

    [HttpGet("games/available/publishers")]
    [ProducesResponseType(200, Type = typeof(bool))]
    [ProducesResponseType(500)]
    public async Task<IActionResult> GetAvailablePublishers()
    {
        var serviceResult = await _gameService.GetGamePublishersAsync();

        if (!serviceResult.IsValid)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        return Ok(serviceResult.Data);
    }
}