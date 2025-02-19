using AutoMapper;
using FluentValidation;
using Playmor_Asp.Application.Common;
using Playmor_Asp.Application.Common.Errors;
using Playmor_Asp.Application.Common.Types;
using Playmor_Asp.Application.DTOs.User;
using Playmor_Asp.Application.DTOs.UserGame;
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

    public async Task<ServiceResult<bool, IError>> DeleteUserGameAsync(int id, int userId)
    {
        var serviceResult = await GetUserGameByIdAsync(id, userId);

        if (!serviceResult.IsValid)
        {
            return new ServiceResult<bool, IError>
            {
                Data = false,
                Errors = serviceResult.Errors
            };
        }

        var userGame = serviceResult.Data;

        // Already checked in the getter but better to check twice should first check change
        if (userGame?.UserId != userId)
        {
            return new ServiceResult<bool, IError>
            {
                Data = false,
                Errors = [new UnauthorizedError("Unauthorized request")]
            };
        }

        var status = await _userGameRepository.DeleteAsync(id);

        if (!status)
        {
            return new ServiceResult<bool, IError>
            {
                Data = false,
                Errors = [new UnexpectedError("Failed to delete userGame")]
            };
        }

        return new ServiceResult<bool, IError> { Data = true };
    }

    public async Task<ServiceResult<UserGameDTO?, IError>> GetUserGameByIdAsync(int id, int userId)
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

        // Only a user can request their own userGame
        if (userGame.UserId != userId)
        {
            return new ServiceResult<UserGameDTO?, IError>
            {
                Data = null,
                Errors = [new UnauthorizedError("Unauthorized request")]
            };
        }

        var userGameDTO = _mapper.Map<UserGameDTO>(userGame);
        return new ServiceResult<UserGameDTO?, IError> { Data = userGameDTO };
    }
    public async Task<ServiceResult<ICollection<UserGameDTO>, IError>> GetUserGamesByUserIdAsync(int clientId)
    {
        if (clientId < 1)
        {
            return new ServiceResult<ICollection<UserGameDTO>, IError>
            {
                Data = [],
                Errors = [new ValidationError("userId", "Invalid userId passed, id has to be greater than 0")]
            };
        }

        var userGames = await _userGameRepository.GetByUserIDAsync(clientId);
        var userGamesDTO = _mapper.Map<ICollection<UserGameDTO>>(userGames);
        var userDTO = _mapper.Map<UserDTO>(_userRepository.GetByID(clientId));
        foreach (var userGameDTO in userGamesDTO)
        {
            userGameDTO.User = userDTO;
        }

        return new ServiceResult<ICollection<UserGameDTO>, IError> { Data = userGamesDTO };
    }

    public async Task<ServiceResult<UserGameStatistics?, IError>> GetUserGamesStatisticsAsync(int userId)
    {
        if (userId < 1)
        {
            return new ServiceResult<UserGameStatistics?, IError>
            {
                Data = null,
                Errors = [new ValidationError("userId", "Invalid userId passed, id has to be greater than 0")]
            };
        }

        var client = _mapper.Map<UserDTO>(_userRepository.GetByID(userId));
        if (client == null)
        {
            return new ServiceResult<UserGameStatistics?, IError>
            {
                Data = null,
                Errors = [new NotFoundError("No user found with given id")]
            };
        }

        var serviceResult = await GetUserGamesByUserIdAsync(userId);

        if (!serviceResult.IsValid)
        {
            return new ServiceResult<UserGameStatistics?, IError>
            { Data = null, Errors = serviceResult.Errors };
        }

        var gamesTotal = serviceResult.Data.Count;

        if (gamesTotal == 0)
        {
            return new ServiceResult<UserGameStatistics?, IError>
            {
                Data = new UserGameStatistics
                {
                    Games = 0,
                    GamesInProgress = 0,
                    GamesCompleted = 0,
                    GamesDropped = 0,
                    GamesPlanned = 0,
                    AverageRating = 0
                }
            };
        }

        var gamesInProgress = 0;
        var gamesCompleted = 0;
        var gamesDropped = 0;
        var gamesPlanned = 0;
        var totalScore = 0;

        foreach (var game in serviceResult.Data)
        {
            if (game.Status == Domain.Enums.UserGameStatus.Playing)
                gamesInProgress++;
            if (game.Status == Domain.Enums.UserGameStatus.Completed)
                gamesCompleted++;
            if (game.Status == Domain.Enums.UserGameStatus.Dropped)
                gamesDropped++;
            if (game.Status == Domain.Enums.UserGameStatus.PlanningToPlay)
                gamesPlanned++;

            totalScore += game.Score;
        }

        var averageRating = Math.Round((double)totalScore / gamesTotal, 2);

        var userGameStatistics = new UserGameStatistics
        {
            Games = gamesTotal,
            GamesInProgress = gamesInProgress,
            GamesCompleted = gamesCompleted,
            GamesDropped = gamesDropped,
            GamesPlanned = gamesPlanned,
            AverageRating = averageRating
        };

        return new ServiceResult<UserGameStatistics?, IError>
        {
            Data = userGameStatistics,
        };
    }
}