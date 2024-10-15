using Playmor_Asp.Application.Common;
using Playmor_Asp.Application.Common.Errors;
using Playmor_Asp.Domain.Models;

namespace Playmor_Asp.Application.Interfaces;

public interface IGameService
{
    Task<ServiceResult<Game?, IError>> GetGameAsync(int id);
    Task<ServiceResult<bool, IError>> CreateGameAsync(Game game);
    Task<ServiceResult<bool, IError>> UpdateGameAsync(int id, Game game);
    Task<ServiceResult<bool, IError>> DeleteGameAsync(int id);
    Task<ServiceResult<ICollection<Game>, IError>> GetGamesAsync();
    Task<ServiceResult<ICollection<Game>, IError>> GetPaginatedGamesAsync(int pageNumber, int pageSize);
    Task<ServiceResult<ICollection<Game>, IError>> GetGamesByAddedDateAsync(string order);
    Task<ServiceResult<ICollection<Game>, IError>> GetGamesByReleaseDateAsync(string order);
    Task<ServiceResult<ICollection<Game>, IError>> GetGamesByTitleAsync(string title);
    Task<ServiceResult<ICollection<Game>, IError>> GetGamesByKeywordAsync(string keyword);
    Task<ServiceResult<ICollection<Game>, IError>> GetGamesByModesAsync(ICollection<string> modes);
    Task<ServiceResult<ICollection<Game>, IError>> GetGamesByGenresAsync(ICollection<string> genres);
}
