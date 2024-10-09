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
        throw new NotImplementedException();
    }

    public bool DeleteGame(int id)
    {
        throw new NotImplementedException();
    }

    public Game? GetGame(int id)
    {
        return _gameRepository.Get(id);
    }

    public bool UpdateGame(int id, Game game)
    {
        return _gameRepository.Update(id, game);
    }
    public ICollection<Game> GetGames()
    {
        return _gameRepository.GetAll();
    }

    public ICollection<Game> GetGamesByAddedDate(string order)
    {
        if (order == "desc") return _gameRepository.GetByAddedDate(Domain.Enums.SortOrder.desc);
        return _gameRepository.GetByAddedDate(Domain.Enums.SortOrder.asc);
    }

    public ICollection<Game> GetGamesByReleaseDate(string order)
    {
        if (order == "desc") return _gameRepository.GetByReleaseDate(Domain.Enums.SortOrder.desc);
        return _gameRepository.GetByReleaseDate(Domain.Enums.SortOrder.asc);
    }

    public ICollection<Game> GetGamesByGenres(ICollection<string> genres)
    {
        return _gameRepository.GetByGenres(genres);
    }

    public ICollection<Game> GetGamesByKeyword(string keyword)
    {
        return _gameRepository.GetByKeyword(keyword);
    }

    public ICollection<Game> GetGamesByModes(ICollection<string> modes)
    {
        return _gameRepository.GetByModes(modes);
    }

    public ICollection<Game> GetGamesByTitle(string title)
    {
        return _gameRepository.GetByTitle(title);
    }

}
