using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Playmor_Asp.Application.Common.Errors;
using Playmor_Asp.Application.DTOs.Notification;
using Playmor_Asp.Application.Services;
using Playmor_Asp.Helpers;

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
