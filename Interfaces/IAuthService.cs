using Playmor_Asp.DTOs;
using Playmor_Asp.Models;
using System.IdentityModel.Tokens.Jwt;

namespace Playmor_Asp.Interfaces
{
    public interface IAuthService
    {
        public string Login(UserLoginDTO userLoginDTO);
        public string Register(UserRegisterDTO userRegisterDTO);
        public string RefreshToken();
        public Tuple<string, JwtSecurityToken> CreateToken(User user);
    }
}
