using Playmor_Asp.Domain.Enums;

namespace Playmor_Asp.Application.DTOs.Notification;

// FriendRequest notification
// System notifications won't need
// a request from the frontend
public class NotificationPostDTO
{
    public int RecipientId { get; set; }
    public int? SenderId { get; set; }
    public NotificationType Type { get; set; }
}
