using Playmor_Asp.Domain.Enums;

namespace Playmor_Asp.Domain.Models;

public class User
{
    public int Id { get; set; }
    public required string Username { get; set; }
    public required byte[] PasswordHash { get; set; }
    public required byte[] PasswordSalt { get; set; }
    public string Token { get; set; }
    public DateTime TokenCreated { get; set; }
    public DateTime TokenExpires { get; set; }
    public required string Email { get; set; }
    public required string PhoneNumber { get; set; }
    public UserRole UserRole { get; set; }
}
