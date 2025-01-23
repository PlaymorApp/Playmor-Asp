using Playmor_Asp.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Playmor_Asp.Domain.Models;

public class Notification
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public required int RecipientId { get; set; }
    public required string Title { get; set; }
    public required string Content { get; set; }
    public required NotificationType Type { get; set; }
    public required bool IsRead { get; set; }
    public int? SenderId { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}
