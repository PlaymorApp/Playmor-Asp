using Playmor_Asp.Domain.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Playmor_Asp.Application.DTOs;

public class UserGameDTO
{
    public int Id { get; set; }
    public int GameId { get; set; }
    public int UserId { get; set; }
    public Game Game { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}
