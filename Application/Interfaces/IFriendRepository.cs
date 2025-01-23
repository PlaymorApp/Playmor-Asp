using Playmor_Asp.Domain.Models;

namespace Playmor_Asp.Application.Interfaces;

public interface IFriendRepository
{
    public Task<ICollection<Friend>> GetByUserIdAsync(int id);
    public Task<Friend?> GetByIdAsync(int id);
    public Task<Friend?> CreateAsync(Friend friend);
    public Task<bool> DeleteAsync(int id);
    public Task<bool> SaveAsync();
}
