using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Playmor_Asp.Application.Common.Errors;
using Playmor_Asp.Application.DTOs.Friend;
using Playmor_Asp.Application.Services;
using Playmor_Asp.Helpers;

namespace Playmor_Asp.Presentation.Controllers;

[Route("api")]
[ApiController]
public class FriendController : Controller
{
    private readonly FriendService _friendService;

    public FriendController(FriendService friendService)
    {
        _friendService = friendService;
    }

    [HttpGet("friends/byUser/{userId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ICollection<FriendDTO>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetFriendsByUserIdAsync([FromRoute] int userId)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var serviceResult = await _friendService.GetFriendsByUserIdAsync(userId);
        if (serviceResult == null)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Unexpected server error");
        }

        if (!serviceResult.IsValid)
        {
            if (serviceResult.Errors.Any(e => e is ValidationError))
            {
                return BadRequest(serviceResult.Errors);
            }

            if (serviceResult.Errors.Any(e => e is UnexpectedError))
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Unexpected server error");
            }
        }

        return Ok(serviceResult.Data);
    }

    [HttpGet("friends/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FriendDTO))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Authorize]
    public async Task<IActionResult> GetFriendByIdAsync([FromRoute] int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var serviceResult = await _friendService.GetFriendByIdAsync(id);
        if (serviceResult == null)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Unexpected server error");
        }

        if (!serviceResult.IsValid)
        {
            if (serviceResult.Errors.Any(e => e is ValidationError))
            {
                return BadRequest(serviceResult.Errors);
            }

            if (serviceResult.Errors.Any(e => e is UnexpectedError))
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Unexpected server error");
            }
        }

        return Ok(serviceResult.Data);
    }

    [HttpPost("friends")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FriendDTO))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Authorize]
    public async Task<IActionResult> PostFriendAsync([FromBody] FriendPostDTO friendPostDTO)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (User.GetUserId() is not int uid)
        {
            return Unauthorized("Unauthorized request");
        }

        var serviceResult = await _friendService.CreateFriendAsync(friendPostDTO);
        if (serviceResult == null)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Unexpected server error");
        }

        if (!serviceResult.IsValid)
        {
            if (serviceResult.Errors.Any(e => e is ValidationError))
            {
                return BadRequest(serviceResult.Errors);
            }

            if (serviceResult.Errors.Any(e => e is UnexpectedError))
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Unexpected server error");
            }
        }

        return Ok(serviceResult.Data);
    }

    [HttpDelete("friends/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Authorize]
    public async Task<IActionResult> DeleteFriendAsync([FromRoute] int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (User.GetUserId() is not int uid)
        {
            return Unauthorized("Unauthorized request received");
        }

        var serviceResult = await _friendService.DeleteFriendAsync(id, uid);

        if (serviceResult == null)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Unexpected server error");
        }

        if (!serviceResult.IsValid)
        {
            if (serviceResult.Errors.Any(e => e is ValidationError))
            {
                return BadRequest(serviceResult.Errors);
            }

            if (serviceResult.Errors.Any(e => e is UnauthorizedError))
            {
                return Unauthorized("Unauthorized request received");
            }

            if (serviceResult.Errors.Any(e => e is UnexpectedError))
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Unexpected server error");
            }
        }

        return Ok(serviceResult.Data);
    }
}