using Playmor_Asp.Domain.Models;

namespace Playmor_Asp.Application.Interfaces;

public interface ICommentRepository
{
    public Task<ICollection<Comment>> GetAllAsync();
    public Task<ICollection<Comment>> GetByGameIdAsync(int id);
    public Task<ICollection<Comment>> GetByReplyIdAsync(int id);
    public Task<Comment?> GetByIdAsync(int id);
    public Task<Comment?> CreateAsync(Comment comment);
    public Task<Comment?> UpdateAsync(Comment comment, int oldCommentId);
    public Task<bool> DeleteAsync(int id);
    public Task<bool> SaveAsync();
}
