using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Playmor_Asp.DTOs;
using Playmor_Asp.Interfaces;
using Playmor_Asp.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Playmor_Asp.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IHashingService _hashingService;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        public AuthService(IUserRepository userRepository, IHashingService hashingService, IMapper mapper, IConfiguration config)
        {
            _userRepository = userRepository;
            _hashingService = hashingService;
            _mapper = mapper;
            _config = config;
        }
        public string Login(UserLoginDTO userLoginDTO)
        {
            var user = _userRepository.GetByUsername(userLoginDTO.Username);
            if (user == null)
                throw new Exception($"User with name {userLoginDTO.Username} not found");

            var hashesMatch = _hashingService.CompareHash(userLoginDTO.Password, user.PasswordHash, user.PasswordSalt);
            if (!hashesMatch)
                throw new Exception("Invalid credentials");

            (var jwt, var token) = CreateToken(user);

            user.Token = jwt;
            user.TokenCreated = token.ValidFrom;
            user.TokenExpires = token.ValidTo;

            _userRepository.Update(user.Id, user, typeof(UserCredentialsDTO));

            return jwt;
        }

        public string Register(UserRegisterDTO userRegisterDTO)
        {
            if (_userRepository.GetByUsername(userRegisterDTO.Username) != null)
                throw new Exception($"User with name {userRegisterDTO.Username} already exists");

            if (_userRepository.GetByEmail(userRegisterDTO.Email) != null)
                throw new Exception($"User with email {userRegisterDTO.Email} already exists");

            (byte[] hash, byte[] salt) = _hashingService.CreateHash(userRegisterDTO.Password);
            
            var user = _mapper.Map<User>(userRegisterDTO);
        
            user.PasswordHash = hash;
            user.PasswordSalt = salt;
            user.UserRole = Enums.UserRole.User;

            (var jwt, var token) = CreateToken(user);

            user.Token = jwt;
            user.TokenCreated = token.ValidFrom;
            user.TokenExpires = token.ValidTo;

            _userRepository.Create(user);

            return jwt; 
        }

        public string RefreshToken()
        {
            throw new NotImplementedException();
        }

        public Tuple<string, JwtSecurityToken> CreateToken(User user)
        {
            List<Claim> claims =
            [
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.UserRole.ToString())
            ];

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _config.GetSection("AppSettings:Token").Value ?? string.Empty));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(
                claims: claims,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: cred
            );
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return new Tuple<string, JwtSecurityToken>(jwt, token);
        }
    }
}
