using Microsoft.EntityFrameworkCore;
using Playmor_Asp.Application.Interfaces;
using Playmor_Asp.Domain.Models;
using Playmor_Asp.Infrastructure.Data;

namespace Playmor_Asp.Infrastructure.Repositories;

public class FriendRepository : IFriendRepository
{
    private readonly DataContext _dataContext;

    public FriendRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<Friend?> CreateAsync(Friend friend)
    {
        var newFriend = (await _dataContext.Friends.AddAsync(friend)).Entity;

        if (newFriend == null)
        {
            return null;
        }

        var status = await SaveAsync();

        return status ? newFriend : null;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var friendToDelete = await _dataContext.Friends.FindAsync(id);

        if (friendToDelete == null)
        {
            return false;
        }

        _dataContext.Friends.Remove(friendToDelete);

        var status = await SaveAsync();

        return status;
    }

    public async Task<Friend?> GetByIdAsync(int id)
    {
        return await _dataContext.Friends.FindAsync(id);
    }

    public async Task<ICollection<Friend>> GetByUserIdAsync(int id)
    {
        return await _dataContext.Friends.Where(f => f.FirstUserId == id || f.SecondUserId == id).ToListAsync();
    }

    public async Task<bool> SaveAsync()
    {
        var written = await _dataContext.SaveChangesAsync();
        return written > 0;
    }
}
