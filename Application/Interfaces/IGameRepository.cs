using Playmor_Asp.Application.Common.Filters;
using Playmor_Asp.Application.Common.Types;
using Playmor_Asp.Domain.Enums;
using Playmor_Asp.Domain.Models;

namespace Playmor_Asp.Application.Interfaces;

public interface IGameRepository
{
    Task<Game?> GetAsync(int id);
    Task<ICollection<Game>> GetAllAsync();
    Task<GamePagination> GetPaginatedAsync(int pageNumber, int pageSize, SortByOrder? sortBy, GameFilter? gameFilter);
    Task<ICollection<Game>> GetByTitleAsync(string title);
    Task<ICollection<Game>> GetByKeywordAsync(string keyword);
    Task<ICollection<Game>> GetByAddedDateAsync(SortOrder sortOrder);
    Task<ICollection<Game>> GetByReleaseDateAsync(SortOrder sortOrder);
    Task<ICollection<Game>> GetByModesAsync(ICollection<string> modes);
    Task<ICollection<Game>> GetByGenresAsync(ICollection<string> genres);
    public Task<bool> CreateAsync(Game game);
    public Task<bool> UpdateAsync(int id, Game game);
    public Task<bool> DeleteAsync(int id);
    public Task<bool> SaveAsync();
}
