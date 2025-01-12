namespace Playmor_Asp.Application.DTOs.Comment;

public class CommentPostDTO
{
    public int GameId { get; set; }
    public int? ReplyId { get; set; }
    public int CommenterId { get; set; }
    public string Content { get; set; }
}
