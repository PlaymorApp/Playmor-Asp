namespace Playmor_Asp.Application.DTOs.Comment;

public class CommentPostDTO
{
    public int GameId { get; set; }
    public int? ReplyId { get; set; }
    public required int CommenterId { get; set; }
    public required string Content { get; set; }
}
