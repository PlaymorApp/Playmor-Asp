using AutoMapper;
using FluentValidation;
using Playmor_Asp.Application.Common;
using Playmor_Asp.Application.Common.Errors;
using Playmor_Asp.Application.DTOs;
using Playmor_Asp.Application.Interfaces;
using Playmor_Asp.Domain.Models;

namespace Playmor_Asp.Application.Services;

public class UserGameService : IUserGameService
{
    private readonly IUserGameRepository _userGameRepository;
    private readonly IUserRepository _userRepository;
    private readonly IGameRepository _gameRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<UserGame> _userGameValidator;

    public UserGameService(IUserGameRepository userGameRepository, IMapper mapper, IValidator<UserGame> userGameValidator, IUserRepository userRepository, IGameRepository gameRepository)
    {
        _userGameRepository = userGameRepository;
        _mapper = mapper;
        _userGameValidator = userGameValidator;
        _userRepository = userRepository;
        _gameRepository = gameRepository;
    }

    public async Task<ServiceResult<UserGameDTO?, IError>> CreateUserGameAsync(UserGamePostDTO userGamePostDTO)
    {
        var userGame = _mapper.Map<UserGame>(userGamePostDTO);

        var user = _userRepository.GetByID(userGame.UserId);
        if (user == null)
        {
            return new ServiceResult<UserGameDTO?, IError>
            {
                Data = null,
                Errors = [new NotFoundError($"No user found with {userGame.UserId} id")]
            };
        }
        userGame.User = user;

        var game = await _gameRepository.GetAsync(userGame.GameId);
        if (game == null)
        {
            return new ServiceResult<UserGameDTO?, IError>
            {
                Data = null,
                Errors = [new NotFoundError($"No game found with {userGame.GameId} id")]
            };
        }
        userGame.Game = game;
        if (!_userGameValidator.Validate(userGame).IsValid)
        {
            return new ServiceResult<UserGameDTO?, IError>
            {
                Data = null,
                Errors = [new ValidationError("UserGame", "Failed to validate userGame")]
            };
        }

        var newUserGame = await _userGameRepository.CreateAsync(userGame);

        if (newUserGame == null)
        {
            return new ServiceResult<UserGameDTO?, IError>
            {
                Data = null,
                Errors = [new UnexpectedError("Encountered unexpected error when attempting to create new userGame")]
            };
        }

        var newUserGameDTO = _mapper.Map<UserGameDTO>(newUserGame);

        return new ServiceResult<UserGameDTO?, IError> { Data = newUserGameDTO };
    }

    public async Task<ServiceResult<bool, IError>> DeleteUserGameAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<ServiceResult<UserGameDTO?, IError>> GetUserGameByIdAsync(int id)
    {
        if (id < 1)
        {
            return new ServiceResult<UserGameDTO?, IError>
            {
                Data = null,
                Errors = [new ValidationError("id", "Invalid id passed, id has to be greater than 0")]
            };
        }

        var userGame = await _userGameRepository.GetByIDAsync(id);
        if (userGame == null)
        {
            return new ServiceResult<UserGameDTO?, IError>
            {
                Data = null,
                Errors = [new NotFoundError("No userGame with such id exists")]
            };
        }
        var userGameDTO = _mapper.Map<UserGameDTO>(userGame);
        return new ServiceResult<UserGameDTO?, IError> { Data = userGameDTO };
    }
    public async Task<ServiceResult<ICollection<UserGameDTO>, IError>> GetUserGamesByUserIdAsync(int userId)
    {
        if (userId < 1)
        {
            return new ServiceResult<ICollection<UserGameDTO>, IError>
            {
                Data = [],
                Errors = [new ValidationError("userId", "Invalid userId passed, id has to be greater than 0")]
            };
        }

        var userGames = await _userGameRepository.GetByUserIDAsync(userId);
        var userGamesDTO = _mapper.Map<ICollection<UserGameDTO>>(userGames);

        return new ServiceResult<ICollection<UserGameDTO>, IError> { Data = userGamesDTO };
    }
}