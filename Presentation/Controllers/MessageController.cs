using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Playmor_Asp.Application.Common.Errors;
using Playmor_Asp.Application.DTOs.Message;
using Playmor_Asp.Application.Interfaces;
using Playmor_Asp.Helpers;
using Swashbuckle.AspNetCore.Annotations;

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
    [SwaggerOperation(
        Summary = "Get message by id",
        Description = "Retrieves message with given id. Requires the auth cookie set."
    )]
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
    [SwaggerOperation(
        Summary = "Get messages of user as a recipient",
        Description = "Retrieves messages of authorized user as a recipient. Requires the auth cookie set."
    )]
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
    [SwaggerOperation(
        Summary = "Get messages of user as a sender",
        Description = "Retrieves messages of authorized user a sender. Requires the auth cookie set."
    )]
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
    [SwaggerOperation(
        Summary = "Post message",
        Description = "Creates a new message resource. Requires the auth cookie set."
    )]
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
    [SwaggerOperation(
        Summary = "Delete message by id",
        Description = "Removes existing message with given id. Requires the auth cookie set."
    )]
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

    [HttpPatch("messages/{id}")]
    [SwaggerOperation(
        Summary = "Patch message by id",
        Description = "Updates message with given id. Used to toggle the isRead status. Requires the auth cookie set."
    )]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MessageDTO))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Authorize]
    public async Task<IActionResult> PatchMessageByIdAsync([FromRoute] int id, [FromBody] MessagePatchDTO messagePatchDTO)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (User.GetUserId() is not int userId)
        {
            return Unauthorized("Unauthorized to access resource");
        }

        var serviceResult = await _messageService.UpdateMessageAsync(messagePatchDTO, userId);

        if (!serviceResult.IsValid)
        {
            string errorsString = string.Join(" ", serviceResult.Errors.Select(e => $"{e.ErrorCode}: {e.Message}"));

            if (serviceResult.Errors.Any(e => e is ValidationError))
            {
                return BadRequest(errorsString);
            }
            else if (serviceResult.Errors.Any(e => e is UnauthorizedError))
            {
                return Unauthorized(errorsString);
            }
            else if (serviceResult.Errors.Any(e => e is NotFoundError))
            {
                return NotFound(errorsString);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, errorsString);
            }
        }

        return Ok(serviceResult.Data);
    }

    [HttpPatch("messages")]
    [SwaggerOperation(
        Summary = "Patch many messages",
        Description = "Updates messages. Used to toggle the isRead status. Requires the auth cookie set."
    )]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MessageDTO))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Authorize]
    public async Task<IActionResult> PatchMessages([FromBody] List<MessagePatchDTO> messagesPatchDTO)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (User.GetUserId() is not int userId)
        {
            return Unauthorized("Unauthorized to access resource");
        }

        var serviceResult = await _messageService.UpdateMessagesAsync(messagesPatchDTO, userId);

        if (!serviceResult.IsValid)
        {
            string errorsString = string.Join(" ", serviceResult.Errors.Select(e => $"{e.ErrorCode}: {e.Message}"));

            if (serviceResult.Errors.Any(e => e is ValidationError))
            {
                return BadRequest(errorsString);
            }
            else if (serviceResult.Errors.Any(e => e is UnauthorizedError))
            {
                return Unauthorized(errorsString);
            }
            else if (serviceResult.Errors.Any(e => e is NotFoundError))
            {
                return NotFound(errorsString);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, errorsString);
            }
        }

        return Ok(serviceResult.Data);
    }
}
