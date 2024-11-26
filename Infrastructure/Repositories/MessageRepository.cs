using Microsoft.EntityFrameworkCore;
using Playmor_Asp.Application.Interfaces;
using Playmor_Asp.Domain.Models;
using Playmor_Asp.Infrastructure.Data;

namespace Playmor_Asp.Infrastructure.Repositories;

public class MessageRepository : IMessageRepository
{
    private readonly DataContext _dataContext;
    public MessageRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<Message?> CreateAsync(Message message)
    {
        var newMessage = (await _dataContext.AddAsync(message)).Entity;

        if (newMessage == null)
        {
            return null;
        }

        var saved = await SaveAsync();

        if (!saved)
        {
            return null;
        }

        return newMessage;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var message = await GetByIDAsync(id);

        if (message == null)
        {
            return false;
        }

        _dataContext.Remove(message);

        return await SaveAsync();
    }

    public async Task<Message?> GetByIDAsync(int id)
    {
        return await _dataContext.Messages.SingleOrDefaultAsync(m => m.Id == id);
    }

    public async Task<ICollection<Message>> GetByRecipientIdAsync(int recipientId)
    {
        return await _dataContext.Messages.Where(m => m.RecipientId == recipientId).ToListAsync();
    }

    public async Task<ICollection<Message>> GetBySenderIdAsync(int senderId)
    {
        return await _dataContext.Messages.Where(m => m.SenderId == senderId).ToListAsync();

    }
    public async Task<bool> SaveAsync()
    {
        return await _dataContext.SaveChangesAsync() > 0;
    }
}
