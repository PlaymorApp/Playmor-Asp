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
    [ProducesResponseType(400)]
    public IActionResult GetGames()
    {
        var games = _gameService.GetGames();

        if (!ModelState.IsValid) { return BadRequest(ModelState); }
        return Ok(games);
    }

    [HttpGet("games/{gameId}")]
    [ProducesResponseType(200, Type = typeof(Game))]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public IActionResult GetGame(int gameId)
    {
        var game = _gameService.GetGame(gameId);
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
    public IActionResult GetGamesByTitle(string title)
    {
        var games = _gameService.GetGamesByTitle(title);

        if (!ModelState.IsValid) { return BadRequest(ModelState); }
        return Ok(games);
    }

    [HttpGet("games/genres")]
    [ProducesResponseType(200, Type = typeof(Game))]
    [ProducesResponseType(400)]
    public IActionResult GetGamesByGenres([FromQuery] ICollection<string> genres)
    {
        var games = _gameService.GetGamesByGenres(genres);

        if (!ModelState.IsValid) { return BadRequest(ModelState); }
        return Ok(games);
    }

    [HttpGet("games/modes")]
    [ProducesResponseType(200, Type = typeof(Game))]
    [ProducesResponseType(400)]
    public IActionResult GetGamesByModes([FromQuery] ICollection<string> modes)
    {
        var games = _gameService.GetGamesByModes(modes);

        if (!ModelState.IsValid) { return BadRequest(ModelState); }
        return Ok(games);
    }

    [HttpGet("games/keyword/{keyword}")]
    [ProducesResponseType(200, Type = typeof(Game))]
    [ProducesResponseType(400)]
    public IActionResult GetGamesByKeyword(string keyword)
    {
        var games = _gameService.GetGamesByKeyword(keyword);

        if (!ModelState.IsValid) { return BadRequest(ModelState); }
        return Ok(games);
    }
}
