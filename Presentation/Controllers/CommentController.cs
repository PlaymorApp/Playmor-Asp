using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Playmor_Asp.Application.Common.Errors;
using Playmor_Asp.Application.DTOs.Comment;
using Playmor_Asp.Application.Interfaces;

namespace Playmor_Asp.Presentation.Controllers;
[Route("api")]
[ApiController]
public class CommentController : Controller
{
    private readonly ICommentService _commentService;

    public CommentController(ICommentService commentService)
    {
        _commentService = commentService;
    }

    [HttpGet("comments")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ICollection<CommentDTO>))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Authorize]
    public async Task<IActionResult> GetAllCommentsAsync()
    {
        var serviceResult = await _commentService.GetAllCommentsAsync();

        if (serviceResult == null)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Unexpected error encoutered." });
        }

        if (!serviceResult.IsValid)
        {
            if (serviceResult.Errors.Any(e => e is UnexpectedError))
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { errors = serviceResult.Errors });
            }
        }

        return Ok(serviceResult.Data);
    }

    [HttpGet("comments/game/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ICollection<CommentDTO>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllCommentsByGameAsync([FromRoute] int gameId)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var serviceResult = await _commentService.GetAllCommentsByGameIdAsync(gameId);

        if (serviceResult == null)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Unexpected error encoutered." });
        }

        if (!serviceResult.IsValid)
        {
            if (serviceResult.Errors.Any(e => e is ValidationError))
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { errors = serviceResult.Errors });
            }
            if (serviceResult.Errors.Any(e => e is NotFoundError))
            {
                return StatusCode(StatusCodes.Status404NotFound, new { errors = serviceResult.Errors });
            }
            if (serviceResult.Errors.Any(e => e is UnexpectedError))
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { errors = serviceResult.Errors });
            }
        }

        return Ok(serviceResult.Data);
    }

    // TODO:
    // Create
    // Update
    // Delete
}
