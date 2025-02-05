using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Playmor_Asp.Application.Common.Errors;
using Playmor_Asp.Application.DTOs.Message;
using Playmor_Asp.Application.Interfaces;
using Playmor_Asp.Helpers;

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

    [HttpGet("messages/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MessageDTO))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Authorize]
    public async Task<IActionResult> GetMessageByIdAsync([FromRoute] int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (User.GetUserId() is not int userId)
        {
            return Unauthorized("Unauthorized to access resource");
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

    [HttpGet("messages/recipient")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ICollection<MessageDTO>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Authorize]
    public async Task<IActionResult> GetMessageByRecipientIdAsync()
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (User.GetUserId() is not int userId)
        {
            return Unauthorized("Unauthorized to access resource");
        }

        var serviceResult = await _messageService.GetMessagesByRecipientIdAsync(userId);

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

    [HttpGet("messages/sender")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ICollection<MessageDTO>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Authorize]
    public async Task<IActionResult> GetMessageBySenderIdAsync()
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (User.GetUserId() is not int userId)
        {
            return Unauthorized("Unauthorized to access resource");
        }

        var serviceResult = await _messageService.GetMessagesBySenderIdAsync(userId);

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

    [HttpPost("messages")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MessageDTO))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Authorize]
    public async Task<IActionResult> CreateMessageAsync([FromBody] MessagePostDTO messagePostDTO)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (User.GetUserId() is not int userId)
        {
            return Unauthorized("Unauthorized to access resource");
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

    [HttpDelete("messages/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(object))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Authorize]
    public async Task<IActionResult> DeleteMessageAsync([FromRoute] int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (User.GetUserId() is not int userId)
        {
            return Unauthorized("Unauthorized to access resource");
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
