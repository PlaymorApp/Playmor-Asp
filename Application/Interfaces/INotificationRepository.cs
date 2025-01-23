using Playmor_Asp.Domain.Models;

namespace Playmor_Asp.Application.Interfaces;

public interface INotificationRepository
{
    public Task<Notification?> GetByIdAsync(int id);
    public Task<int> GetUnreadCountByRecipientIdAsync(int recipientId);
    public Task<ICollection<Notification>> GetByRecipientIdAsync(int recipientId);
    public Task<ICollection<Notification>> GetBySenderIdAsync(int senderId);
    public Task<Notification?> CreateAsync(Notification notification);
    public Task<Notification?> DeleteAsync(int id);
    public Task<bool> SaveAsync();
}
