using AutoMapper;
using FluentValidation;
using Playmor_Asp.Application.Common;
using Playmor_Asp.Application.Common.Errors;
using Playmor_Asp.Application.DTOs.Notification;
using Playmor_Asp.Application.Interfaces;
using Playmor_Asp.Domain.Models;

namespace Playmor_Asp.Application.Services;

public class NotificationService : INotificationService
{
    private readonly INotificationRepository _notificationRepository;
    private readonly IValidator<Notification> _validator;
    private readonly IMapper _mapper;

    public NotificationService(INotificationRepository notificationRepository, IValidator<Notification> validator, IMapper mapper)
    {
        _notificationRepository = notificationRepository;
        _validator = validator;
        _mapper = mapper;
    }

    public async Task<ServiceResult<NotificationDTO?, IError>> CreateNotificationAsync(NotificationPostDTO notificationPostDTO)
    {
        var mappedNotification = _mapper.Map<Notification>(notificationPostDTO);

        var validateStatus = await _validator.ValidateAsync(mappedNotification);

        if (!validateStatus.IsValid)
        {
            var errorsString = string.Join(" ", validateStatus.Errors.Select(e => $"{e.PropertyName}: {e.ErrorMessage}"));

            return new ServiceResult<NotificationDTO?, IError>
            {
                Data = null,
                Errors = [new ValidationError(nameof(notificationPostDTO), errorsString)]
            };
        }

        var createdNotification = await _notificationRepository.CreateAsync(mappedNotification);

        if (createdNotification is null)
        {
            return new ServiceResult<NotificationDTO?, IError>
            {
                Data = null,
                Errors = [new UnexpectedError("Unexpected server error.")]
            };
        }

        var mappedNotificationDTO = _mapper.Map<NotificationDTO>(createdNotification);

        return new ServiceResult<NotificationDTO?, IError> { Data = mappedNotificationDTO };
    }
    public async Task<ServiceResult<NotificationDTO?, IError>> DeleteNotificationAsync(int id, int userId)
    {
        if (id < 1)
        {
            return new ServiceResult<NotificationDTO?, IError>
            {
                Data = null,
                Errors = [new ValidationError(nameof(id), $"Validation failed: {nameof(id)} can't be lower than 1")]
            };
        }

        if (userId < 1)
        {
            return new ServiceResult<NotificationDTO?, IError>
            {
                Data = null,
                Errors = [new ValidationError(nameof(userId), $"Validation failed: {nameof(userId)} can't be lower than 1")]
            };
        }

        var notificationToDelete = await _notificationRepository.GetByIdAsync(id);

        if (notificationToDelete is null)
        {
            return new ServiceResult<NotificationDTO?, IError>
            {
                Data = null,
                Errors = [new NotFoundError($"Failed to find notification with id: {id}")]
            };
        }

        if (notificationToDelete.RecipientId != userId)
        {
            return new ServiceResult<NotificationDTO?, IError>
            {
                Data = null,
                Errors = [new UnauthorizedError("Unauthorized operation.")]
            };
        }

        var deletedNotification = await _notificationRepository.DeleteAsync(id);

        if (deletedNotification is null)
        {
            return new ServiceResult<NotificationDTO?, IError>
            {
                Data = null,
                Errors = [new UnexpectedError("Unexpected server error.")]
            };
        }

        var mappedNotification = _mapper.Map<NotificationDTO>(deletedNotification);

        return new ServiceResult<NotificationDTO?, IError> { Data = mappedNotification };
    }

    public async Task<ServiceResult<NotificationDTO?, IError>> GetNotificationByIdAsync(int id)
    {
        if (id < 1)
        {
            return new ServiceResult<NotificationDTO?, IError>
            {
                Data = null,
                Errors = [new ValidationError(nameof(id), $"Validation failed: {nameof(id)} can't be lower than 1")]
            };
        }

        var notification = await _notificationRepository.GetByIdAsync(id);

        if (notification is null)
        {
            return new ServiceResult<NotificationDTO?, IError>
            {
                Data = null,
                Errors = [new NotFoundError($"Failed to find notification with id: {id}")]
            };
        }

        var mappedNotification = _mapper.Map<NotificationDTO?>(notification);

        return new ServiceResult<NotificationDTO?, IError> { Data = mappedNotification };
    }

    public async Task<ServiceResult<ICollection<NotificationDTO>, IError>> GetNotificationsByRecipientIdAsync(int recipientId)
    {
        if (recipientId < 1)
        {
            return new ServiceResult<ICollection<NotificationDTO>, IError>
            {
                Data = [],
                Errors = [new ValidationError(nameof(recipientId), $"Validation failed: {nameof(recipientId)} can't be lower than 1")]
            };
        }

        var notifications = await _notificationRepository.GetByRecipientIdAsync(recipientId);

        if (notifications is null)
        {
            return new ServiceResult<ICollection<NotificationDTO>, IError> { Data = [], Errors = [new UnexpectedError("Unexpected server error.")] };
        }

        var mappedNotifications = _mapper.Map<ICollection<NotificationDTO>>(notifications);

        return new ServiceResult<ICollection<NotificationDTO>, IError> { Data = mappedNotifications };
    }

    public async Task<ServiceResult<ICollection<NotificationDTO>, IError>> GetNotificationsBySenderIdAsync(int senderId)
    {
        if (senderId < 1)
        {
            return new ServiceResult<ICollection<NotificationDTO>, IError>
            {
                Data = [],
                Errors = [new ValidationError(nameof(senderId), $"Validation failed: {nameof(senderId)} can't be lower than 1")]
            };
        }

        var notifications = await _notificationRepository.GetBySenderIdAsync(senderId);

        if (notifications is null)
        {
            return new ServiceResult<ICollection<NotificationDTO>, IError> { Data = [], Errors = [new UnexpectedError("Unexpected server error.")] };
        }

        var mappedNotifications = _mapper.Map<ICollection<NotificationDTO>>(notifications);

        return new ServiceResult<ICollection<NotificationDTO>, IError> { Data = mappedNotifications };
    }

    public async Task<ServiceResult<int, IError>> GetUnreadNotificationCountByRecipientIdAsync(int recipientId)
    {
        var sR = await GetNotificationsByRecipientIdAsync(recipientId);

        if (!sR.IsValid)
        {
            return new ServiceResult<int, IError>
            {
                Data = 0,
                Errors = sR.Errors
            };
        }

        var unreadCount = sR.Data.Where(n => n.IsRead == false).Count();

        return new ServiceResult<int, IError> { Data = unreadCount };
    }
}
