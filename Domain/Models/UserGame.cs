using System.ComponentModel.DataAnnotations.Schema;

namespace Playmor_Asp.Domain.Models;

public class UserGame
{
    public int Id { get; set; }
    public int GameId { get; set; }
    public int UserId { get; set; }
    public Game Game { get; set; }
    public User User { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}
