namespace Playmor_Asp.Application.DTOs;

public class UserCredentialsDTO
{
    public string Token { get; set; }
    public DateTime TokenCreated { get; set; }
    public DateTime TokenExpires { get; set; }
}
