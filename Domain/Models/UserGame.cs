using Playmor_Asp.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Playmor_Asp.Domain.Models;

public class UserGame
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public required int GameId { get; set; }
    public required int UserId { get; set; }
    public required int Score { get; set; }
    public required UserGameStatus Status { get; set; }
    public required Game Game { get; set; }
    public required User User { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}
