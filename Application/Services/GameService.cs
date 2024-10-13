using Microsoft.IdentityModel.Tokens;
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

    public async Task<bool> CreateGameAsync(Game game)
    {
        return await _gameRepository.CreateAsync(game);
    }

    public async Task<bool> DeleteGameAsync(int id)
    {
        if (id < 1)
        {
            throw new ArgumentException("Invalid argument passed. Id can't be lower than 1");
        }

        return await _gameRepository.DeleteAsync(id);
    }

    public async Task<Game?> GetGameAsync(int id)
    {
        if (id < 1)
        {
            throw new ArgumentException("Invalid argument passed. Id can't be lower than 1");
        }

        return await _gameRepository.GetAsync(id);
    }

    public async Task<bool> UpdateGameAsync(int id, Game game)
    {
        if (id < 1)
        {
            throw new ArgumentException("Invalid argument passed. Id can't be lower than 1");
        }

        if (await _gameRepository.GetAsync(id) == null)
        {
            throw new InvalidOperationException("Invalid argument passed. No game found under such id.");
        }

        return await _gameRepository.UpdateAsync(id, game);
    }

    public async Task<ICollection<Game>> GetGamesAsync()
    {
        return await _gameRepository.GetAllAsync();
    }

    public async Task<ICollection<Game>> GetPaginatedGamesAsync(int pageNumber, int pageSize)
    {
        if (pageNumber < 1 || pageSize < 1)
        {
            throw new ArgumentException("Invalid argument passed. PageNumber and PageSize should be >= 1");
        }

        return await _gameRepository.GetPaginatedAsync(pageNumber, pageSize);
    }

    public async Task<ICollection<Game>> GetGamesByAddedDateAsync(string order)
    {
        if (order != "desc" && order != "asc")
        {
            throw new ArgumentException("Invalid argument passed. Order has to be 'asc' or 'desc'");
        }

        if (order == "desc") return await _gameRepository.GetByAddedDateAsync(Domain.Enums.SortOrder.desc);
        return await _gameRepository.GetByAddedDateAsync(Domain.Enums.SortOrder.asc);
    }

    public async Task<ICollection<Game>> GetGamesByReleaseDateAsync(string order)
    {
        if (order != "desc" && order != "asc")
        {
            throw new ArgumentException("Invalid argument passed. Order has to be 'asc' or 'desc'");
        }

        if (order == "desc") return await _gameRepository.GetByReleaseDateAsync(Domain.Enums.SortOrder.desc);
        return await _gameRepository.GetByReleaseDateAsync(Domain.Enums.SortOrder.asc);
    }

    public async Task<ICollection<Game>> GetGamesByGenresAsync(ICollection<string> genres)
    {
        if (genres.IsNullOrEmpty())
        {
            throw new ArgumentException("Invalid argument passed. Genres can't be null or empty");
        }

        return await _gameRepository.GetByGenresAsync(genres);
    }

    public async Task<ICollection<Game>> GetGamesByKeywordAsync(string keyword)
    {
        if (keyword.IsNullOrEmpty())
        {
            throw new ArgumentException("Invalid argument passed. Keyword can't be null or empty");
        }

        return await _gameRepository.GetByKeywordAsync(keyword);
    }

    public async Task<ICollection<Game>> GetGamesByModesAsync(ICollection<string> modes)
    {
        if (modes.IsNullOrEmpty())
        {
            throw new ArgumentException("Invalid argument passed. Modes can't be null or empty");
        }

        return await _gameRepository.GetByModesAsync(modes);
    }

    public async Task<ICollection<Game>> GetGamesByTitleAsync(string title)
    {

        return await _gameRepository.GetByTitleAsync(title);
    }
}
