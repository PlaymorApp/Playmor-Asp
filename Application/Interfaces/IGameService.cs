using Playmor_Asp.Domain.Models;

namespace Playmor_Asp.Application.Interfaces;

public interface IGameService
{
    Task<Game?> GetGameAsync(int id);
    Task<bool> CreateGameAsync(Game game);
    Task<bool> UpdateGameAsync(int id, Game game);
    Task<bool> DeleteGameAsync(int id);
    Task<ICollection<Game>> GetGamesAsync();
    Task<ICollection<Game>> GetPaginatedGamesAsync(int pageNumber, int pageSize);
    Task<ICollection<Game>> GetGamesByAddedDateAsync(string order);
    Task<ICollection<Game>> GetGamesByReleaseDateAsync(string order);
    Task<ICollection<Game>> GetGamesByTitleAsync(string title);
    Task<ICollection<Game>> GetGamesByKeywordAsync(string keyword);
    Task<ICollection<Game>> GetGamesByModesAsync(ICollection<string> modes);
    Task<ICollection<Game>> GetGamesByGenresAsync(ICollection<string> genres);
}
