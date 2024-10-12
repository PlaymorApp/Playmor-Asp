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

    public bool CreateGame(Game game)
    {
        return _gameRepository.Create(game);
    }

    public bool DeleteGame(int id)
    {
        if (id < 1)
        {
            throw new ArgumentException("Invalid argument passed. Id can't be lower than 1");
        }

        return _gameRepository.Delete(id);
    }

    public Game? GetGame(int id)
    {
        if (id < 1)
        {
            throw new ArgumentException("Invalid argument passed. Id can't be lower than 1");
        }

        return _gameRepository.Get(id);
    }

    public bool UpdateGame(int id, Game game)
    {
        if (id < 1)
        {
            throw new ArgumentException("Invalid argument passed. Id can't be lower than 1");
        }

        if (_gameRepository.Get(id) == null)
        {
            throw new InvalidOperationException("Invalid argument passed. No game found under such id.");
        }

        return _gameRepository.Update(id, game);
    }

    public ICollection<Game> GetGames()
    {
        return _gameRepository.GetAll();
    }

    public ICollection<Game> GetPaginatedGames(int pageNumber, int pageSize)
    {
        if (pageNumber < 1 || pageSize < 1)
        {
            throw new ArgumentException("Invalid argument passed. PageNumber and PageSize should be >= 1");
        }

        return _gameRepository.GetPaginated(pageNumber, pageSize);
    }

    public ICollection<Game> GetGamesByAddedDate(string order)
    {
        if (order != "desc" && order != "asc")
        {
            throw new ArgumentException("Invalid argument passed. Order has to be 'asc' or 'desc'");
        }

        if (order == "desc") return _gameRepository.GetByAddedDate(Domain.Enums.SortOrder.desc);
        return _gameRepository.GetByAddedDate(Domain.Enums.SortOrder.asc);
    }

    public ICollection<Game> GetGamesByReleaseDate(string order)
    {
        if (order != "desc" && order != "asc")
        {
            throw new ArgumentException("Invalid argument passed. Order has to be 'asc' or 'desc'");
        }

        if (order == "desc") return _gameRepository.GetByReleaseDate(Domain.Enums.SortOrder.desc);
        return _gameRepository.GetByReleaseDate(Domain.Enums.SortOrder.asc);
    }

    public ICollection<Game> GetGamesByGenres(ICollection<string> genres)
    {
        if (genres.IsNullOrEmpty())
        {
            throw new ArgumentException("Invalid argument passed. Genres can't be null or empty");
        }

        return _gameRepository.GetByGenres(genres);
    }

    public ICollection<Game> GetGamesByKeyword(string keyword)
    {
        if (keyword.IsNullOrEmpty())
        {
            throw new ArgumentException("Invalid argument passed. Keyword can't be null or empty");
        }

        return _gameRepository.GetByKeyword(keyword);
    }

    public ICollection<Game> GetGamesByModes(ICollection<string> modes)
    {
        if (modes.IsNullOrEmpty())
        {
            throw new ArgumentException("Invalid argument passed. Modes can't be null or empty");
        }

        return _gameRepository.GetByModes(modes);
    }

    public ICollection<Game> GetGamesByTitle(string title)
    {

        return _gameRepository.GetByTitle(title);
    }
}
