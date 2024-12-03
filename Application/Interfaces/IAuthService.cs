using Playmor_Asp.Application.Common;
using Playmor_Asp.Application.Common.Errors;
using Playmor_Asp.Application.DTOs.User;
using Playmor_Asp.Domain.Models;
using System.IdentityModel.Tokens.Jwt;

namespace Playmor_Asp.Application.Interfaces;

public interface IAuthService
{
    public ServiceResult<string, IError> Login(UserAuthDTO userLoginDTO);

    public ServiceResult<string, IError> Register(UserPostDTO userRegisterDTO);

    public (string, JwtSecurityToken) CreateToken(User user);
}
