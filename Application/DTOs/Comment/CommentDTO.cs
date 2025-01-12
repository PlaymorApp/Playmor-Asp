using Playmor_Asp.Application.DTOs.User;

namespace Playmor_Asp.Application.DTOs.Comment;

public class CommentDTO
{
    public int Id { get; set; }
    public int? ReplyId { get; set; }
    public int GameId { get; set; }
    public int CommenterId { get; set; }
    public int Score { get; set; }
    public string Content { get; set; }
    public UserDTO Commenter { get; set; }
    public DateTime CreatedAt { get; set; }
}
