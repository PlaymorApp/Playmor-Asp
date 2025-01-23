using Playmor_Asp.Domain.Enums;

namespace Playmor_Asp.Application.DTOs.Notification;

public class NotificationDTO
{
    public int Id { get; set; }
    public int RecipientId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public NotificationType Type { get; set; }
    public bool IsRead { get; set; }
    public int? SenderId { get; set; }
    public DateTime CreatedAt { get; set; }
}
