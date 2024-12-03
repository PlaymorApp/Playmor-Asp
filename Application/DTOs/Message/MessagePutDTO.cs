namespace Playmor_Asp.Application.DTOs.Message;

public class MessagePutDTO
{
    public int Id { get; set; }
    public int RecipientId { get; set; }
    public int SenderId { get; set; }
    public string Content { get; set; }
    public bool IsRead { get; set; }
}
