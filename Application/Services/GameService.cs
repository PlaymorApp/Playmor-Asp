using AutoMapper;
using FluentValidation;
using Microsoft.IdentityModel.Tokens;
using Playmor_Asp.Application.Common;
using Playmor_Asp.Application.Common.Errors;
using Playmor_Asp.Application.Common.Filters;
using Playmor_Asp.Application.Common.Types;
using Playmor_Asp.Application.DTOs.Game;
using Playmor_Asp.Application.Interfaces;
using Playmor_Asp.Domain.Enums;
using Playmor_Asp.Domain.Models;

namespace Playmor_Asp.Application.Services;

public class GameService : IGameService
{
    private readonly IGameRepository _gameRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<Game> _gameValidator;

    public GameService(IGameRepository gameRepository, IMapper mapper, IValidator<Game> gameValidator)
    {
        _gameRepository = gameRepository;
        _mapper = mapper;
        _gameValidator = gameValidator;
    }

    public async Task<ServiceResult<bool, IError>> CreateGameAsync(GamePostDTO game)
    {
        if (game == null)
        {
            return new ServiceResult<bool, IError>
            {
                Data = false,
                Errors = [new ValidationError("game", "Empty game passed")]
            };

        }

        var mappedGame = _mapper.Map<Game>(game);
        if (!_gameValidator.Validate(mappedGame).IsValid)
        {
            return new ServiceResult<bool, IError>
            {
                Data = false,
                Errors = [new ValidationError("game", "Invalid game object passed")]
            };
        }

        var created = await _gameRepository.CreateAsync(mappedGame);
        if (created)
        {
            return new ServiceResult<bool, IError>
            {
                Data = created
            };
        }

        return new ServiceResult<bool, IError>
        {
            Data = false,
            Errors = [new UnexpectedError($"Server error occured when attempting to create game: {game}")]
        };
    }

    public async Task<ServiceResult<bool, IError>> DeleteGameAsync(int id)
    {
        if (id < 1)
        {
            return new ServiceResult<bool, IError>
            {
                Data = false,
                Errors = [new ValidationError("id", $"Incorrect id passed: {id}")]
            };
        }

        if ((await _gameRepository.GetAsync(id)) == null)
        {
            return new ServiceResult<bool, IError>
            {
                Data = false,
                Errors = [new NotFoundError("No game found with passed id.")]
            };
        }

        var status = await _gameRepository.DeleteAsync(id);
        if (status)
        {
            return new ServiceResult<bool, IError>
            {
                Data = status
            };
        }

        return new ServiceResult<bool, IError>
        {
            Data = false,
            Errors = [new UnexpectedError($"Server error occured when attempting to delete game with id: {id}")]
        };
    }

    public async Task<ServiceResult<Game?, IError>> GetGameAsync(int id)
    {
        if (id < 1)
        {
            return new ServiceResult<Game?, IError>
            {
                Data = null,
                Errors = [new ValidationError("id", "Incorrect id passed")]
            };
        }

        var game = await _gameRepository.GetAsync(id);
        if (game == null)
        {
            return new ServiceResult<Game?, IError>
            {
                Data = null,
                Errors = [new NotFoundError($"Game with id: {id} not found")]
            };
        }

        return new ServiceResult<Game?, IError>
        {
            Data = game
        };
    }

    public async Task<ServiceResult<bool, IError>> UpdateGameAsync(int id, GameDTO game)
    {
        if (id < 1)
        {
            return new ServiceResult<bool, IError>
            {
                Data = false,
                Errors = [new ValidationError("id", "Incorrect id passed")]
            };
        }

        if ((await _gameRepository.GetAsync(id)) == null)
        {
            return new ServiceResult<bool, IError>
            {
                Data = false,
                Errors = [new NotFoundError("No game found with passed id.")]
            };
        }

        var mappedGame = _mapper.Map<Game>(game);
        if (!_gameValidator.Validate(mappedGame).IsValid)
        {
            return new ServiceResult<bool, IError>
            {
                Data = false,
                Errors = [new ValidationError("game", "Invalid game object passed")]
            };
        }

        var status = await _gameRepository.UpdateAsync(id, mappedGame);
        if (status)
        {
            return new ServiceResult<bool, IError>
            {
                Data = status
            };
        }

        return new ServiceResult<bool, IError>
        {
            Data = false,
            Errors = [new UnexpectedError($"Server error occured when attempting to update game with id: {id}")]
        };
    }

    public async Task<ServiceResult<ICollection<Game>, IError>> GetGamesAsync()
    {
        var games = await _gameRepository.GetAllAsync();

        if (games.IsNullOrEmpty())
        {
            return new ServiceResult<ICollection<Game>, IError>
            {
                Data = [],
                Errors = [new NotFoundError("Games couldn't be found")]
            };
        }

        return new ServiceResult<ICollection<Game>, IError>
        {
            Data = games
        };
    }

    public async Task<ServiceResult<GamePagination, IError>> GetPaginatedGamesAsync(int pageNumber, int pageSize, SortByOrder? sortBy, GameFilter? gameFilter)
    {
        if (pageNumber < 1 || pageSize < 1)
        {
            return new ServiceResult<GamePagination, IError>
            {
                Data = new GamePagination { Games = [], TotalRecords = 0, TotalPages = 0 },
                Errors = [new ValidationError("pageNumber | pageSize", "Value lower than 1 passed.")]
            };
        }


        if (sortBy.HasValue && !Enum.IsDefined(typeof(SortByOrder), sortBy))
        {
            return new ServiceResult<GamePagination, IError>
            {
                Data = new GamePagination { Games = [], TotalRecords = 0, TotalPages = 0 },
                Errors =
                [
                    new ValidationError("sortBy", "Invalid sortBy value passed.")
                ]
            };
        }

        var gamePagination = await _gameRepository.GetPaginatedAsync(pageNumber, pageSize, sortBy, gameFilter);

        return new ServiceResult<GamePagination, IError>
        {
            Data = gamePagination
        };
    }

    public async Task<ServiceResult<ICollection<Game>, IError>> GetGamesByAddedDateAsync(string order)
    {
        if (order != "desc" && order != "asc")
        {
            return new ServiceResult<ICollection<Game>, IError>
            {
                Data = [],
                Errors = [new ValidationError("order", "Order can only be 'asc' or 'desc'.")]
            };
        }

        ICollection<Game> games;

        if (order == "desc")
        {
            games = await _gameRepository.GetByAddedDateAsync(Domain.Enums.SortOrder.desc);

        }
        else
        {
            games = await _gameRepository.GetByAddedDateAsync(Domain.Enums.SortOrder.asc);
        }

        if (games.IsNullOrEmpty())
        {
            return new ServiceResult<ICollection<Game>, IError>
            {
                Data = [],
                Errors = [new UnexpectedError("Unexpected server error occurred when attempting to return games by added date")]
            };
        }

        return new ServiceResult<ICollection<Game>, IError>
        {
            Data = games,
        };
    }

    public async Task<ServiceResult<ICollection<Game>, IError>> GetGamesByReleaseDateAsync(string order)
    {
        if (order != "desc" && order != "asc")
        {
            return new ServiceResult<ICollection<Game>, IError>
            {
                Data = [],
                Errors = [new ValidationError("order", "Order can only be 'asc' or 'desc'.")]
            };
        }

        ICollection<Game> games;

        if (order == "desc")
        {
            games = await _gameRepository.GetByReleaseDateAsync(Domain.Enums.SortOrder.desc);

        }
        else
        {
            games = await _gameRepository.GetByReleaseDateAsync(Domain.Enums.SortOrder.asc);
        }

        if (games.IsNullOrEmpty())
        {
            return new ServiceResult<ICollection<Game>, IError>
            {
                Data = [],
                Errors = [new UnexpectedError("Unexpected server error occurred when attempting to return games by release date")]
            };
        }

        return new ServiceResult<ICollection<Game>, IError>
        {
            Data = games,
        };
    }

    public async Task<ServiceResult<ICollection<Game>, IError>> GetGamesByGenresAsync(ICollection<string> genres)
    {
        if (genres.IsNullOrEmpty())
        {
            return new ServiceResult<ICollection<Game>, IError>
            {
                Data = [],
                Errors = [new ValidationError("genres", "Genres can't be null or empty.")]
            };
        }

        var games = await _gameRepository.GetByGenresAsync(genres);
        if (games.IsNullOrEmpty())
        {
            return new ServiceResult<ICollection<Game>, IError>
            {
                Data = [],
                Errors = [new UnexpectedError("Unexpected server error occurred when attempting to return games by genres")]
            };
        }

        return new ServiceResult<ICollection<Game>, IError>
        {
            Data = games
        };
    }

    public async Task<ServiceResult<ICollection<Game>, IError>> GetGamesByKeywordAsync(string keyword)
    {
        if (keyword.IsNullOrEmpty())
        {
            return new ServiceResult<ICollection<Game>, IError>
            {
                Data = [],
                Errors = [new ValidationError("genres", "Genres can't be null or empty.")]
            };
        }

        var games = await _gameRepository.GetByKeywordAsync(keyword);
        if (games.IsNullOrEmpty())
        {
            return new ServiceResult<ICollection<Game>, IError>
            {
                Data = [],
                Errors = [new UnexpectedError("Unexpected server error occurred when attempting to return games by keyword")]
            };
        }

        return new ServiceResult<ICollection<Game>, IError>
        {
            Data = games
        };
    }

    public async Task<ServiceResult<ICollection<Game>, IError>> GetGamesByModesAsync(ICollection<string> modes)
    {
        if (modes.IsNullOrEmpty())
        {
            return new ServiceResult<ICollection<Game>, IError>
            {
                Data = [],
                Errors = [new ValidationError("modes", "Modes can't be null or empty.")]
            };
        }

        var games = await _gameRepository.GetByModesAsync(modes);
        if (games.IsNullOrEmpty())
        {
            return new ServiceResult<ICollection<Game>, IError>
            {
                Data = [],
                Errors = [new UnexpectedError("Unexpected server error occurred when attempting to return games by modes")]
            };
        }

        return new ServiceResult<ICollection<Game>, IError>
        {
            Data = games
        };
    }

    public async Task<ServiceResult<ICollection<Game>, IError>> GetGamesByTitleAsync(string title)
    {
        if (!string.IsNullOrEmpty(title))
        {
            return new ServiceResult<ICollection<Game>, IError>
            {
                Data = [],
                Errors = [new ValidationError("title", "Title can't be null or empty.")]
            };
        }

        var games = await _gameRepository.GetByTitleAsync(title);
        if (games.IsNullOrEmpty())
        {
            return new ServiceResult<ICollection<Game>, IError>
            {
                Data = [],
                Errors = [new UnexpectedError("Unexpected server error occurred when attempting to return games by title")]
            };
        }

        return new ServiceResult<ICollection<Game>, IError>
        {
            Data = games
        };
    }

    public async Task<ServiceResult<ICollection<string>, IError>> GetGameModesAsync()
    {
        var modes = await _gameRepository.GetModesAsync();

        if (modes.IsNullOrEmpty())
        {
            return new ServiceResult<ICollection<string>, IError>
            {
                Data = [],
                Errors = [new UnexpectedError("Unexpected server error occurred when attempting to return modes")]
            };
        }

        return new ServiceResult<ICollection<string>, IError>
        {
            Data = modes
        };
    }

    public async Task<ServiceResult<ICollection<string>, IError>> GetGameGenresAsync()
    {
        var genres = await _gameRepository.GetGenresAsync();

        if (genres.IsNullOrEmpty())
        {
            return new ServiceResult<ICollection<string>, IError>
            {
                Data = [],
                Errors = [new UnexpectedError("Unexpected server error occurred when attempting to return genres")]
            };
        }

        return new ServiceResult<ICollection<string>, IError>
        {
            Data = genres
        };
    }

    public async Task<ServiceResult<ICollection<string>, IError>> GetGamePlatformsAsync()
    {
        var platforms = await _gameRepository.GetPlatformsAsync();

        if (platforms.IsNullOrEmpty())
        {
            return new ServiceResult<ICollection<string>, IError>
            {
                Data = [],
                Errors = [new UnexpectedError("Unexpected server error occurred when attempting to return platforms")]
            };
        }

        return new ServiceResult<ICollection<string>, IError>
        {
            Data = platforms
        };
    }

    public async Task<ServiceResult<ICollection<string>, IError>> GetGameDevelopersAsync()
    {
        var developers = await _gameRepository.GetDevelopersAsync();

        if (developers.IsNullOrEmpty())
        {
            return new ServiceResult<ICollection<string>, IError>
            {
                Data = [],
                Errors = [new UnexpectedError("Unexpected server error occurred when attempting to return developers")]
            };
        }

        return new ServiceResult<ICollection<string>, IError>
        {
            Data = developers
        };
    }

    public async Task<ServiceResult<ICollection<string>, IError>> GetGamePublishersAsync()
    {
        var publishers = await _gameRepository.GetPublishersAsync();

        if (publishers.IsNullOrEmpty())
        {
            return new ServiceResult<ICollection<string>, IError>
            {
                Data = [],
                Errors = [new UnexpectedError("Unexpected server error occurred when attempting to return publishers")]
            };
        }

        return new ServiceResult<ICollection<string>, IError>
        {
            Data = publishers
        };
    }
}
