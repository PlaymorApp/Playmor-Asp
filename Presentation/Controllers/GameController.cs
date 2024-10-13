using Microsoft.AspNetCore.Mvc;
using Playmor_Asp.Application.Interfaces;
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
        var games = await _gameService.GetGamesAsync();

        return Ok(games);
    }

    [HttpGet("games/paginated")]
    [ProducesResponseType(200, Type = typeof(ICollection<Game>))]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> GetPaginatedGamesAsync([FromQuery] int pageNumber, [FromQuery] int pageSize)
    {
        var games = await _gameService.GetPaginatedGamesAsync(pageNumber, pageSize);
        if (!ModelState.IsValid) { return BadRequest(ModelState); }
        return Ok(games);
    }

    [HttpGet("games/added")]
    [ProducesResponseType(200, Type = typeof(ICollection<Game>))]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> GetGamesByAddedDateAsync([FromQuery] string sort)
    {
        var games = await _gameService.GetGamesByAddedDateAsync(sort);

        if (!ModelState.IsValid) { return BadRequest(ModelState); }
        return Ok(games);
    }

    [HttpGet("games/released")]
    [ProducesResponseType(200, Type = typeof(ICollection<Game>))]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> GetGamesByReleaseDateAsync([FromQuery] string sort)
    {
        var games = await _gameService.GetGamesByReleaseDateAsync(sort);

        if (!ModelState.IsValid) { return BadRequest(ModelState); }
        return Ok(games);
    }

    [HttpGet("games/{gameId}")]
    [ProducesResponseType(200, Type = typeof(Game))]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> GetGameAsync(int gameId)
    {
        var game = await _gameService.GetGameAsync(gameId);
        if (game == null)
        {
            return NotFound();
        }
        if (!ModelState.IsValid) { return BadRequest(ModelState); }
        return Ok(game);
    }

    [HttpGet("games/title/{title}")]
    [ProducesResponseType(200, Type = typeof(Game))]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> GetGamesByTitleAsync(string title)
    {
        var games = await _gameService.GetGamesByTitleAsync(title);

        if (!ModelState.IsValid) { return BadRequest(ModelState); }
        return Ok(games);
    }

    [HttpGet("games/genres")]
    [ProducesResponseType(200, Type = typeof(Game))]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> GetGamesByGenresAsync([FromQuery] ICollection<string> genres)
    {
        var games = await _gameService.GetGamesByGenresAsync(genres);

        if (!ModelState.IsValid) { return BadRequest(ModelState); }
        return Ok(games);
    }

    [HttpGet("games/modes")]
    [ProducesResponseType(200, Type = typeof(Game))]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> GetGamesByModesAsync([FromQuery] ICollection<string> modes)
    {
        var games = await _gameService.GetGamesByModesAsync(modes);

        if (!ModelState.IsValid) { return BadRequest(ModelState); }
        return Ok(games);
    }

    [HttpGet("games/keyword/{keyword}")]
    [ProducesResponseType(200, Type = typeof(Game))]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> GetGamesByKeywordAsync(string keyword)
    {
        var games = await _gameService.GetGamesByKeywordAsync(keyword);

        if (!ModelState.IsValid) { return BadRequest(ModelState); }
        return Ok(games);
    }
}
