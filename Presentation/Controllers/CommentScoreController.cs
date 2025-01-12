using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Playmor_Asp.Application.Common.Errors;
using Playmor_Asp.Application.DTOs.CommentScore;
using Playmor_Asp.Application.Interfaces;
using Playmor_Asp.Helpers;

namespace Playmor_Asp.Presentation.Controllers;

[Route("api")]
[ApiController]
public class CommentScoreController : Controller
{
    private readonly ICommentScoreService _commentScoreService;

    public CommentScoreController(ICommentScoreService commentScoreService)
    {
        _commentScoreService = commentScoreService;
    }

    [HttpGet("likes/comment/{commentId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetCommentScore([FromRoute] int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var serviceResult = await _commentScoreService.GetCommentScoreTotalAsync(id);

        if (!serviceResult.IsValid)
        {
            if (serviceResult.Errors.Any(e => e is ValidationError))
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { errors = serviceResult.Errors });
            }

            if (serviceResult.Errors.Any(e => e is UnexpectedError))
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { errors = serviceResult.Errors });
            }
        }

        return Ok(serviceResult.Data);
    }

    [HttpPost("likes")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CommentScoreDTO))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Authorize]
    public async Task<IActionResult> PostCommentScore([FromBody] CommentScorePostDTO commentScorePostDTO)
    {
        if (!ModelState.IsValid) { return BadRequest(ModelState); }

        if (User.GetUserId() is not int uid)
        {
            return Unauthorized("Unauthorized to access resource");
        }
        var serviceResult = await _commentScoreService.CreateCommentScoreAsync(commentScorePostDTO, uid);

        if (!serviceResult.IsValid)
        {
            if (serviceResult.Errors.Any(e => e is ValidationError))
            {
                return BadRequest(serviceResult.Errors);
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
}
