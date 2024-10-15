using Microsoft.IdentityModel.Tokens;
using Playmor_Asp.Application.Common;
using Playmor_Asp.Application.Common.Errors;
using Playmor_Asp.Application.Interfaces;
using Playmor_Asp.Domain.Models;

namespace Playmor_Asp.Application.Services;

public class GameService : IGameService
{
    private readonly IGameRepository _gameRepository;

    public GameService(IGameRepository gameRepository)
    {
        _gameRepository = gameRepository;
    }

    public async Task<ServiceResult<bool, IError>> CreateGameAsync(Game game)
    {
        if (game == null)
        {
            return new ServiceResult<bool, IError>
            {
                Data = false,
                Errors = [new ValidationError("game", "Empty game passed")]
            };

        }

        var created = await _gameRepository.CreateAsync(game);

        return new ServiceResult<bool, IError>
        {
            Data = created
        };
    }

    public async Task<ServiceResult<bool, IError>> DeleteGameAsync(int id)
    {
        if (id < 1)
        {
            return new ServiceResult<bool, IError>
            {
                Data = false,
                Errors = [new ValidationError("id", "Incorrect id passed")]
            };
        }


        return new ServiceResult<bool, IError>
        {
            Data = await _gameRepository.DeleteAsync(id)
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

    public async Task<ServiceResult<bool, IError>> UpdateGameAsync(int id, Game game)
    {
        if (id < 1)
        {
            return new ServiceResult<bool, IError>
            {
                Data = false,
                Errors = [new ValidationError("id", "Incorrect id passed")]
            };
        }

        if (await _gameRepository.GetAsync(id) == null)
        {
            return new ServiceResult<bool, IError>
            {
                Data = false,
                Errors = [new NotFoundError("No game found with passed id.")]
            };
        }

        return new ServiceResult<bool, IError>
        {
            Data = await _gameRepository.UpdateAsync(id, game),
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

    public async Task<ServiceResult<ICollection<Game>, IError>> GetPaginatedGamesAsync(int pageNumber, int pageSize)
    {
        if (pageNumber < 1 || pageSize < 1)
        {
            return new ServiceResult<ICollection<Game>, IError>
            {
                Data = [],
                Errors = [new ValidationError("pageNumber | pageSize", "Value lower than 1 passed.")]
            };
        }

        return new ServiceResult<ICollection<Game>, IError>
        {
            Data = await _gameRepository.GetPaginatedAsync(pageNumber, pageSize)
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

        if (order == "desc")
        {
            return new ServiceResult<ICollection<Game>, IError>
            {
                Data = await _gameRepository.GetByAddedDateAsync(Domain.Enums.SortOrder.desc),
            };
        }

        return new ServiceResult<ICollection<Game>, IError>
        {
            Data = await _gameRepository.GetByAddedDateAsync(Domain.Enums.SortOrder.asc),
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

        if (order == "desc")
        {
            return new ServiceResult<ICollection<Game>, IError>
            {
                Data = await _gameRepository.GetByReleaseDateAsync(Domain.Enums.SortOrder.desc),
            };
        }

        return new ServiceResult<ICollection<Game>, IError>
        {
            Data = await _gameRepository.GetByReleaseDateAsync(Domain.Enums.SortOrder.asc),
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

        return new ServiceResult<ICollection<Game>, IError>
        {
            Data = await _gameRepository.GetByGenresAsync(genres),
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

        return new ServiceResult<ICollection<Game>, IError>
        {
            Data = await _gameRepository.GetByKeywordAsync(keyword),
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

        return new ServiceResult<ICollection<Game>, IError>
        {
            Data = await _gameRepository.GetByModesAsync(modes),
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

        return new ServiceResult<ICollection<Game>, IError>
        {
            Data = await _gameRepository.GetByTitleAsync(title),
        };
    }
}
