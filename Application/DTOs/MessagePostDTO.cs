namespace Playmor_Asp.Application.DTOs;

public class MessagePostDTO
{
    public int RecipientId { get; set; }
    public int SenderId { get; set; }
    public string Content { get; set; }
}
