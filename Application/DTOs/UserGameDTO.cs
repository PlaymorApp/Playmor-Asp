using Playmor_Asp.Domain.Models;

namespace Playmor_Asp.Application.DTOs;

public class UserGameDTO
{
    public int Id { get; set; }
    public int GameId { get; set; }
    public int UserId { get; set; }
    public Game Game { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}
