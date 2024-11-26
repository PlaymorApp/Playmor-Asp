using Playmor_Asp.Domain.Models;

namespace Playmor_Asp.Application.Interfaces;

public interface IMessageRepository
{
    public Task<Message?> GetByIDAsync(int id);
    public Task<ICollection<Message>> GetByRecipientIdAsync(int recipientId);
    public Task<ICollection<Message>> GetBySenderIdAsync(int senderId);
    public Task<Message?> CreateAsync(Message message);
    public Task<bool> DeleteAsync(int id);
    public Task<bool> SaveAsync();
}
