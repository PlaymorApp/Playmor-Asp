using Playmor_Asp.Application.Common;
using Playmor_Asp.Application.Common.Errors;
using Playmor_Asp.Application.DTOs.Notification;

namespace Playmor_Asp.Application.Interfaces;

public interface INotificationService
{
    public Task<ServiceResult<NotificationDTO?, IError>> GetNotificationByIdAsync(int id);
    public Task<ServiceResult<int, IError>> GetUnreadNotificationCountByRecipientIdAsync(int recipientId, int userId);
    public Task<ServiceResult<ICollection<NotificationDTO>, IError>> GetNotificationsByRecipientIdAsync(int recipientId, int userId);
    public Task<ServiceResult<ICollection<NotificationDTO>, IError>> GetNotificationsBySenderIdAsync(int senderId, int userId);
    public Task<ServiceResult<NotificationDTO?, IError>> CreateNotificationAsync(NotificationPostDTO notificationPostDTO);
    public Task<ServiceResult<NotificationDTO?, IError>> DeleteNotificationAsync(int id, int userId);
}
