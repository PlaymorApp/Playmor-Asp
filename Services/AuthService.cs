using Playmor_Asp.DTOs;
using Playmor_Asp.Interfaces;

namespace Playmor_Asp.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IHashingService _hashingService;
        public AuthService(IUserRepository userRepository, IHashingService hashingService) 
        {
            _userRepository = userRepository;
            _hashingService = hashingService;
        }
        public string Login(UserLoginDTO userLoginDTO)
        {
            var user = _userRepository.GetByUsername(userLoginDTO.Username);
            if (user == null)
                throw new Exception($"User with name {userLoginDTO.Username} not found");

            var hashesMatch = _hashingService.CompareHash(userLoginDTO.Password, user.PasswordHash, user.PasswordSalt);
            if (!hashesMatch)
                throw new Exception("Invalid credentials");

            var token = CreateToken();
            return token;
        }

        public string Register(UserRegisterDTO userRegisterDTO)
        {
            throw new NotImplementedException();
        }

        public string RefreshToken()
        {
            throw new NotImplementedException();
        }

        public string CreateToken()
        {
            throw new NotImplementedException();
        }
    }
}
