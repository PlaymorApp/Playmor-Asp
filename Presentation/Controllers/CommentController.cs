using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Playmor_Asp.Application.Common.Errors;
using Playmor_Asp.Application.DTOs.Comment;
using Playmor_Asp.Application.Interfaces;
using Playmor_Asp.Helpers;

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

    [HttpPost("/comments")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CommentDTO))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateCommentAsync([FromBody] CommentPostDTO commentPostDTO)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        if (User.GetUserId() is not int userId)
        {
            return Unauthorized("Unauthorized to access resource");
        }

        var serviceResult = await _commentService.CreateCommentAsync(commentPostDTO, userId);

        if (!serviceResult.IsValid)
        {
            if (serviceResult.Errors.Any(e => e is UnauthorizedError))
            {
                return Unauthorized("Unauthorized Request.");
            }

            if (serviceResult.Errors.Any(e => e is ValidationError))
            {
                return BadRequest(serviceResult.Errors);
            }

            if (serviceResult.Errors.Any(e => e is UnexpectedError))
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Unexpected server error.");
            }
        }

        return Ok(serviceResult.Data);
    }

    [HttpPut("/comments/{commentId}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CommentDTO))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateCommentAsync([FromRoute] int commentId, [FromBody] CommentPutDTO commentPutDTO)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        if (User.GetUserId() is not int userId)
        {
            return Unauthorized("Unauthorized to access resource");
        }

        var serviceResult = await _commentService.UpdateCommentAsync(commentPutDTO, userId);

        if (!serviceResult.IsValid)
        {
            if (serviceResult.Errors.Any(e => e is UnauthorizedError))
            {
                return Unauthorized("Unauthorized Request.");
            }

            if (serviceResult.Errors.Any(e => e is ValidationError))
            {
                return BadRequest(serviceResult.Errors);
            }

            if (serviceResult.Errors.Any(e => e is UnexpectedError))
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Unexpected server error.");
            }
        }

        return Ok(serviceResult.Data);
    }

    [HttpDelete("/comments/{commentId}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteCommentAsync([FromRoute] int commentId)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        if (User.GetUserId() is not int userId)
        {
            return Unauthorized("Unauthorized to access resource");
        }

        var serviceResult = await _commentService.DeleteCommentAsync(commentId, userId);

        if (!serviceResult.IsValid)
        {
            if (serviceResult.Errors.Any(e => e is UnauthorizedError))
            {
                return Unauthorized("Unauthorized Request.");
            }

            if (serviceResult.Errors.Any(e => e is ValidationError))
            {
                return BadRequest(serviceResult.Errors);
            }

            if (serviceResult.Errors.Any(e => e is UnexpectedError))
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Unexpected server error.");
            }
        }

        return Ok(serviceResult.Data);
    }

}
