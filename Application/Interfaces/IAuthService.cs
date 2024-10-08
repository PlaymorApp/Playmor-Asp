using Playmor_Asp.Application.DTOs;
using Playmor_Asp.Domain.Models;
using System.IdentityModel.Tokens.Jwt;

namespace Playmor_Asp.Application.Interfaces;

public interface IAuthService
{
    public string Login(UserLoginDTO userLoginDTO);

    public string Register(UserRegisterDTO userRegisterDTO);

    public (string, JwtSecurityToken) CreateToken(User user);
}
