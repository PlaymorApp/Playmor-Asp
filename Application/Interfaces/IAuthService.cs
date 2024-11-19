using Playmor_Asp.Application.Common;
using Playmor_Asp.Application.Common.Errors;
using Playmor_Asp.Application.DTOs;
using Playmor_Asp.Domain.Models;
using System.IdentityModel.Tokens.Jwt;

namespace Playmor_Asp.Application.Interfaces;

public interface IAuthService
{
    public ServiceResult<string, IError> Login(UserLoginDTO userLoginDTO);

    public ServiceResult<string, IError> Register(UserRegisterDTO userRegisterDTO);

    public (string, JwtSecurityToken) CreateToken(User user);
}
