using Playmor_Asp.Application.DTOs.User;

namespace Playmor_Asp.Application.DTOs.Message;

public class MessageDTO
{
    public int Id { get; set; }
    public int RecipientId { get; set; }
    public int SenderId { get; set; }
    public string Content { get; set; }
    public bool IsRead { get; set; }
    public UserDTO? Sender { get; set; }
    public UserDTO? Recipient { get; set; }
    public DateTime CreatedAt { get; set; }
}
