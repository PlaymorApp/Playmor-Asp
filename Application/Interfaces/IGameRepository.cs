using Playmor_Asp.Domain.Enums;
using Playmor_Asp.Domain.Models;

namespace Playmor_Asp.Application.Interfaces;

public interface IGameRepository
{
    Task<Game?> GetAsync(int id);
    Task<ICollection<Game>> GetAllAsync();
    Task<ICollection<Game>> GetPaginatedAsync(int pageNumber, int pageSize);
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
