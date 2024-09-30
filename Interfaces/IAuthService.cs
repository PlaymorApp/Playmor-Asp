using Playmor_Asp.DTOs;

namespace Playmor_Asp.Interfaces
{
    public interface IAuthService
    {
        public string Login(UserLoginDTO userLoginDTO);
        public string Register(UserRegisterDTO userRegisterDTO);
        public string RefreshToken();
        public string CreateToken();
        
    }
}
