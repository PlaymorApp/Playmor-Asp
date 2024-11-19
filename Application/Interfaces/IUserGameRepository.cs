using Playmor_Asp.Domain.Models;

namespace Playmor_Asp.Application.Interfaces;

public interface IUserGameRepository
{
    public Task<UserGame?> GetByIDAsync(int id);
    public Task<ICollection<UserGame>> GetByUserIDAsync(int userId);
    public Task<UserGame?> CreateAsync(UserGame userGame);
    public Task<bool> DeleteAsync(int id);
    public Task<bool> SaveAsync();
}
