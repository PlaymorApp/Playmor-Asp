using Playmor_Asp.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Playmor_Asp.Domain.Models;

public class User
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public required string Username { get; set; }
    public required byte[] PasswordHash { get; set; }
    public required byte[] PasswordSalt { get; set; }
    public string Token { get; set; }
    public DateTime TokenCreated { get; set; }
    public DateTime TokenExpires { get; set; }
    public required string Email { get; set; }
    public string PhoneNumber { get; set; }
    public UserRole UserRole { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}
