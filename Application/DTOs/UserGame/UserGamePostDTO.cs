using Playmor_Asp.Domain.Enums;

namespace Playmor_Asp.Application.DTOs.UserGame;

public class UserGamePostDTO
{
    public int GameId { get; set; }
    public int UserId { get; set; }
    public int Score { get; set; }
    public UserGameStatus Status { get; set; }
}
