using Microsoft.EntityFrameworkCore;
using Playmor_Asp.Application.Interfaces;
using Playmor_Asp.Domain.Models;
using Playmor_Asp.Infrastructure.Data;

namespace Playmor_Asp.Infrastructure.Repositories;

public class NotificationRepository : INotificationRepository
{
    private readonly DataContext _dataContext;
    public NotificationRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<Notification?> CreateAsync(Notification notification)
    {
        var newNotification = (await _dataContext.AddAsync(notification)).Entity;

        if (newNotification == null)
        {
            return null;
        }

        var status = await SaveAsync();

        return status ? newNotification : null;
    }

    public async Task<Notification?> DeleteAsync(int id)
    {
        var notificationToDelete = await _dataContext.Notifications.FindAsync(id);

        if (notificationToDelete == null)
        {
            return null;
        }

        _dataContext.Notifications.Remove(notificationToDelete);

        var status = await SaveAsync();

        return status ? notificationToDelete : null;
    }

    public async Task<Notification?> GetByIdAsync(int id)
    {
        return await _dataContext.Notifications.FindAsync(id);
    }

    public async Task<ICollection<Notification>> GetByRecipientIdAsync(int recipientId)
    {
        return await _dataContext.Notifications.Where(n => n.RecipientId == recipientId).ToListAsync();
    }

    public async Task<ICollection<Notification>> GetBySenderIdAsync(int senderId)
    {
        return await _dataContext.Notifications.Where(n => n.SenderId == senderId).ToListAsync();
    }

    public async Task<int> GetUnreadCountByRecipientIdAsync(int recipientId)
    {
        return (await GetByRecipientIdAsync(recipientId)).Count;
    }

    public async Task<bool> SaveAsync()
    {
        return (await _dataContext.SaveChangesAsync()) > 0;
    }
}
