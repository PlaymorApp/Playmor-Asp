namespace Playmor_Asp.Application.DTOs.UserGame;
using Game = Domain.Models.Game;
public class UserGameDTO
{
    public int Id { get; set; }
    public int GameId { get; set; }
    public int UserId { get; set; }
    public Game Game { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}
