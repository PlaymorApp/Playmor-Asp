namespace Playmor_Asp.Application.DTOs.CommentScore;

public class CommentScoreDTO
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int CommentId { get; set; }
    public int Value { get; set; }
    public DateTime CreatedAt { get; set; }
}
