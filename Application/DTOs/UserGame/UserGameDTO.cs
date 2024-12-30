namespace Playmor_Asp.Application.DTOs.UserGame;

using Playmor_Asp.Application.DTOs.User;
using Playmor_Asp.Domain.Enums;
using Game = Domain.Models.Game;
public class UserGameDTO
{
    public int Id { get; set; }
    public int GameId { get; set; }
    public int UserId { get; set; }
    public int Score { get; set; }
    public UserGameStatus Status { get; set; }
    public Game Game { get; set; }
    public UserDTO User { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}
