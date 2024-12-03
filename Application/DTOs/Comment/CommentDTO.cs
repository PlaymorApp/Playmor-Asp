using Playmor_Asp.Application.DTOs.User;

namespace Playmor_Asp.Application.DTOs.Comment;

public class CommentDTO
{
    public int Id { get; set; }
    public int? ReplyId { get; set; }
    public int GameId { get; set; }
    public required int CommenterId { get; set; }
    public required string Content { get; set; }
    public required UserDTO Commenter { get; set; }
    public DateTime CreatedAt { get; set; }
}
