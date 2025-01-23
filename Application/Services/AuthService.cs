using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using Playmor_Asp.Application.Common;
using Playmor_Asp.Application.Common.Errors;
using Playmor_Asp.Application.DTOs.User;
using Playmor_Asp.Application.Interfaces;
using Playmor_Asp.Domain.Enums;
using Playmor_Asp.Domain.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Playmor_Asp.Application.Services;

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
    public ServiceResult<string, IError> Login(UserAuthDTO userLoginDTO)
    {
        var user = _userRepository.GetByEmail(userLoginDTO.Email);
        if (user == null)
        {
            return new ServiceResult<string, IError>
            {
                Data = "",
                Errors = [new NotFoundError($"User with email {userLoginDTO.Email} not found")]
            };
        }

        var hashesMatch = _hashingService.CompareHash(userLoginDTO.Password, user.PasswordHash, user.PasswordSalt);
        if (!hashesMatch)
        {
            return new ServiceResult<string, IError>
            {
                Data = "",
                Errors = [new ValidationError("Incorrect login credentials", "Failed to login with passed credentials")]
            };
        }

        (var jwt, var token) = CreateToken(user);

        user.Token = jwt;
        user.TokenCreated = token.ValidFrom;
        user.TokenExpires = token.ValidTo;

        var status = _userRepository.Update(user.Id, user, typeof(UserTokenDTO));
        if (status == null)
        {
            return new ServiceResult<string, IError> { Data = "", Errors = [new UnexpectedError("Unexpected server error.")] };
        }

        return new ServiceResult<string, IError> { Data = jwt };
    }

    public ServiceResult<string, IError> Register(UserPostDTO userRegisterDTO)
    {
        if (_userRepository.GetByUsername(userRegisterDTO.Username) != null)
        {
            return new ServiceResult<string, IError>
            {
                Data = "",
                Errors = [new ValidationError("Username", $"User with name {userRegisterDTO.Username} already exists")]
            };
        }

        if (_userRepository.GetByEmail(userRegisterDTO.Email) != null)
        {
            return new ServiceResult<string, IError>
            {
                Data = "",
                Errors = [new NotFoundError($"User with email {userRegisterDTO.Email} already exists")]
            };
        }

        (byte[] hash, byte[] salt) = _hashingService.CreateHash(userRegisterDTO.Password);

        var user = _mapper.Map<User>(userRegisterDTO);

        user.PasswordHash = hash;
        user.PasswordSalt = salt;
        user.UserRole = UserRole.User;

        (var jwt, var token) = CreateToken(user);

        user.Token = jwt;
        user.TokenCreated = token.ValidFrom;
        user.TokenExpires = token.ValidTo;

        var status = _userRepository.Create(user);
        if (status == null)
        {
            return new ServiceResult<string, IError> { Data = "", Errors = [new UnexpectedError("Unexpected server error.")] };
        }
        return new ServiceResult<string, IError> { Data = jwt };
    }

    public (string, JwtSecurityToken) CreateToken(User user)
    {
        List<Claim> claims =
        [
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
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
        return (jwt, token);
    }
}
