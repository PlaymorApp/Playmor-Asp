using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Playmor_Asp.Application.Common.Errors;
using Playmor_Asp.Application.DTOs.Notification;
using Playmor_Asp.Application.Services;
using Playmor_Asp.Helpers;
using Swashbuckle.AspNetCore.Annotations;

namespace Playmor_Asp.Presentation.Controllers;

[Route("api")]
[ApiController]
public class NotificationController : Controller
{
    private readonly NotificationService _notificationService;

    public NotificationController(NotificationService notificationService)
    {
        _notificationService = notificationService;
    }

    [HttpGet("notifications/{id}")]
    [SwaggerOperation(
        Summary = "Get notification by id",
        Description = "Retrieves a notification based on its id. Requires the auth cookie set."
    )]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(NotificationDTO))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Authorize]
    public async Task<IActionResult> GetNotificationByIdAsync([FromRoute] int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var serviceResult = await _notificationService.GetNotificationByIdAsync(id);

        if (!serviceResult.IsValid)
        {
            if (serviceResult.Errors.Any(e => e is ValidationError))
            {
                return BadRequest($"{nameof(id)} validation failed.");
            }

            if (serviceResult.Errors.Any(e => e is UnauthorizedError))
            {
                return Unauthorized("Unauthorized request received.");
            }

            if (serviceResult.Errors.Any(e => e is UnexpectedError))
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Unexpected server error.");
            }
        }

        return Ok(serviceResult.Data);
    }

    [HttpGet("notifications/unread")]
    [SwaggerOperation(
        Summary = "Get unread notification count",
        Description = "Retrieves a number of unread notifications of authorized user. Requires the auth cookie set."
    )]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Authorize]
    public async Task<IActionResult> GetUnreadNotificationCountAsync()
    {
        if (User.GetUserId() is not int uid)
        {
            return Unauthorized("Unauthorized request received");
        }

        var serviceResult = await _notificationService.GetUnreadNotificationCountByRecipientIdAsync(uid);

        if (!serviceResult.IsValid)
        {
            if (serviceResult.Errors.Any(e => e is ValidationError))
            {
                return BadRequest($"{nameof(uid)} validation failed.");
            }

            if (serviceResult.Errors.Any(e => e is UnexpectedError))
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Unexpected server error.");
            }
        }


        return Ok(serviceResult.Data);
    }

    [HttpGet("notifications")]
    [SwaggerOperation(
        Summary = "Get notifications of user as a recipient",
        Description = "Retrieves notifications of authorized user as recipient. Requires the auth cookie set."
    )]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ICollection<NotificationDTO>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Authorize]
    public async Task<IActionResult> GetNotificationsByRecipientAsync()
    {
        if (User.GetUserId() is not int uid)
        {
            return Unauthorized("Unauthorized request received");
        }

        var serviceResult = await _notificationService.GetNotificationsByRecipientIdAsync(uid);

        if (!serviceResult.IsValid)
        {
            if (serviceResult.Errors.Any(e => e is ValidationError))
            {
                return BadRequest($"{nameof(uid)} validation failed.");
            }

            if (serviceResult.Errors.Any(e => e is UnexpectedError))
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Unexpected server error.");
            }
        }

        return Ok(serviceResult.Data);
    }

    [HttpGet("notifications/sent")]
    [SwaggerOperation(
        Summary = "Get notifications of user as a sender",
        Description = "Retrieves notifications of authorized user as a sender. Requires the auth cookie set."
    )]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ICollection<NotificationDTO>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Authorize]
    public async Task<IActionResult> GetNotificationsBySenderAsync()
    {
        if (User.GetUserId() is not int uid)
        {
            return Unauthorized("Unauthorized request received");
        }

        var serviceResult = await _notificationService.GetNotificationsBySenderIdAsync(uid);

        if (!serviceResult.IsValid)
        {
            if (serviceResult.Errors.Any(e => e is ValidationError))
            {
                return BadRequest($"{nameof(uid)} validation failed.");
            }

            if (serviceResult.Errors.Any(e => e is UnexpectedError))
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Unexpected server error.");
            }
        }

        return Ok(serviceResult.Data);
    }

    [HttpPost("notifications")]
    [SwaggerOperation(
        Summary = "Post notification",
        Description = "Creates a new notification resource. Requires the auth cookie set."
    )]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(NotificationDTO))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Authorize]
    public async Task<IActionResult> PostNotificationAsync([FromBody] NotificationPostDTO notification)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var serviceResult = await _notificationService.CreateNotificationAsync(notification);


        if (!serviceResult.IsValid)
        {
            if (serviceResult.Errors.Any(e => e is ValidationError))
            {
                return BadRequest($"{nameof(notification)} validation failed.");
            }

            if (serviceResult.Errors.Any(e => e is UnexpectedError))
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Unexpected server error.");
            }
        }

        return Ok(serviceResult.Data);
    }

    [HttpDelete("notifications/{id}")]
    [SwaggerOperation(
        Summary = "Delete notification by id",
        Description = "Removes existing notification with given id. Requires the auth cookie set."
    )]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(NotificationDTO))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Authorize]
    public async Task<IActionResult> DeleteNotificationAsync([FromRoute] int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (User.GetUserId() is not int uid)
        {
            return Unauthorized("Unauthorized request received.");
        }

        var serviceResult = await _notificationService.DeleteNotificationAsync(id, uid);

        if (!serviceResult.IsValid)
        {
            if (serviceResult.Errors.Any(e => e is ValidationError))
            {
                return BadRequest($"{nameof(id)} validation failed.");
            }

            if (serviceResult.Errors.Any(e => e is UnauthorizedError))
            {
                return Unauthorized("Unauthorized request received.");
            }

            if (serviceResult.Errors.Any(e => e is UnexpectedError))
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Unexpected server error.");
            }
        }

        return Ok(serviceResult.Data);
    }
}
