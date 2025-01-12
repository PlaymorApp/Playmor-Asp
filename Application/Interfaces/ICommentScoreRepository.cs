using Playmor_Asp.Domain.Models;

namespace Playmor_Asp.Application.Interfaces;

public interface ICommentScoreRepository
{
    public Task<ICollection<CommentScore>> GetAllAsync();
    public Task<ICollection<CommentScore>> GetByCommentIdAsync(int id);
    public Task<CommentScore?> GetByIdAsync(int id);
    public Task<CommentScore?> CreateAsync(CommentScore commentScore);
    public Task<bool> DeleteAsync(int id);
    public Task<bool> SaveAsync();
}
