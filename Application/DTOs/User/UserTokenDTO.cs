namespace Playmor_Asp.Application.DTOs.User;

public class UserTokenDTO
{
    public string Token { get; set; }
    public DateTime TokenCreated { get; set; }
    public DateTime TokenExpires { get; set; }
}
