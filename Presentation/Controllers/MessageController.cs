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
public class MessageController : Controller
{
    private readonly IMessageService _messageService;
    public MessageController(IMessageService messageService)
    {
        _messageService = messageService;
    }

    [Authorize]
    [HttpGet("messages/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Message))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetMessageByIdAsync([FromRoute] int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out var userId))
        {
            return Unauthorized(new { Message = "Invalid or missing user ID." });
        }

        var serviceResult = await _messageService.GetMessageByIDAsync(id, userId);

        if (!serviceResult.IsValid)
        {
            if (serviceResult.Errors.Any(e => e is ValidationError))
            {
                return BadRequest(serviceResult.Errors);
            }
            else if (serviceResult.Errors.Any(e => e is NotFoundError))
            {
                return NotFound(serviceResult.Errors);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, serviceResult.Errors);
            }
        }

        return Ok(serviceResult.Data);
    }

    [Authorize]
    [HttpGet("messages/recipient/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Message))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetMessageByRecipientIdAsync([FromRoute] int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out var userId))
        {
            return Unauthorized(new { Message = "Invalid or missing user ID." });
        }

        var serviceResult = await _messageService.GetMessagesByRecipientIdAsync(id, userId);

        if (!serviceResult.IsValid)
        {
            if (serviceResult.Errors.Any(e => e is ValidationError))
            {
                return BadRequest(serviceResult.Errors);
            }
            else if (serviceResult.Errors.Any(e => e is UnauthorizedError))
            {
                return Unauthorized("Unauthorized to access resource");
            }
            else if (serviceResult.Errors.Any(e => e is NotFoundError))
            {
                return NotFound(serviceResult.Errors);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, serviceResult.Errors);
            }
        }

        return Ok(serviceResult.Data);
    }

    [Authorize]
    [HttpGet("messages/sender/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Message))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetMessageBySenderIdAsync([FromRoute] int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out var userId))
        {
            return Unauthorized(new { Message = "Invalid or missing user ID." });
        }

        var serviceResult = await _messageService.GetMessagesBySenderIdAsync(id, userId);

        if (!serviceResult.IsValid)
        {
            if (serviceResult.Errors.Any(e => e is ValidationError))
            {
                return BadRequest(serviceResult.Errors);
            }
            else if (serviceResult.Errors.Any(e => e is UnauthorizedError))
            {
                return Unauthorized("Unauthorized to access resource");
            }
            else if (serviceResult.Errors.Any(e => e is NotFoundError))
            {
                return NotFound(serviceResult.Errors);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, serviceResult.Errors);
            }
        }

        return Ok(serviceResult.Data);
    }

    [Authorize]
    [HttpPost("messages")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Message))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateMessageAsync([FromBody] MessagePostDTO messagePostDTO)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out var userId))
        {
            return Unauthorized(new { Message = "Invalid or missing user ID." });
        }

        var serviceResult = await _messageService.CreateMessageAsync(messagePostDTO, userId);

        if (!serviceResult.IsValid)
        {
            if (serviceResult.Errors.Any(e => e is ValidationError))
            {
                return BadRequest(serviceResult.Errors);
            }
            else if (serviceResult.Errors.Any(e => e is UnauthorizedError))
            {
                return Unauthorized("Unauthorized to access resource");
            }
            else if (serviceResult.Errors.Any(e => e is NotFoundError))
            {
                return NotFound(serviceResult.Errors);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, serviceResult.Errors);
            }
        }

        return Ok(serviceResult.Data);
    }

    [Authorize]
    [HttpDelete("messages/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteMessageAsync([FromRoute] int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out var userId))
        {
            return Unauthorized(new { Message = "Invalid or missing user ID." });
        }

        var serviceResult = await _messageService.DeleteMessageAsync(id, userId);

        if (!serviceResult.IsValid)
        {
            if (serviceResult.Errors.Any(e => e is ValidationError))
            {
                return BadRequest(serviceResult.Errors);
            }
            else if (serviceResult.Errors.Any(e => e is UnauthorizedError))
            {
                return Unauthorized("Unauthorized to access resource");
            }
            else if (serviceResult.Errors.Any(e => e is NotFoundError))
            {
                return NotFound(serviceResult.Errors);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, serviceResult.Errors);
            }
        }

        return Ok(new { message = "Message deleted successfully" });
    }
}
