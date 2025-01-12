using Microsoft.EntityFrameworkCore;
using Playmor_Asp.Application.Interfaces;
using Playmor_Asp.Domain.Models;
using Playmor_Asp.Helpers;
using Playmor_Asp.Infrastructure.Data;

namespace Playmor_Asp.Infrastructure.Repositories;

public class CommentRepository : ICommentRepository
{
    private readonly DataContext _dataContext;

    public CommentRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<Comment?> CreateAsync(Comment comment)
    {
        var newComment = (await _dataContext.Comments.AddAsync(comment)).Entity;

        if (newComment == null)
        {
            return null;
        }

        var status = await SaveAsync();

        return status ? newComment : null;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var comment = await GetByIdAsync(id);

        if (comment == null)
        {
            return false;
        }

        _dataContext.Remove(comment);

        return await SaveAsync();
    }

    public async Task<ICollection<Comment>> GetAllAsync()
    {
        return await _dataContext.Comments.ToListAsync();
    }

    public async Task<ICollection<Comment>> GetByGameIdAsync(int id)
    {
        return await _dataContext.Comments
            .Where(c => c.GameId == id)
            .OrderByDescending(c => c.CreatedAt)
            .ToListAsync();
    }

    public async Task<ICollection<Comment>> GetByReplyIdAsync(int id)
    {
        return await _dataContext.Comments
            .Where(c => c.ReplyId == id)
            .OrderByDescending(c => c.CreatedAt)
            .ToListAsync();
    }

    public async Task<Comment?> GetByIdAsync(int id)
    {
        return await _dataContext.Comments.FindAsync(id);
    }

    public async Task<bool> SaveAsync()
    {
        var entriesWritten = await _dataContext.SaveChangesAsync();
        return entriesWritten > 0;
    }

    public async Task<Comment?> UpdateAsync(Comment comment, int oldCommentId)
    {
        var oldComment = await GetByIdAsync(oldCommentId);

        if (oldComment == null)
        {
            return null;
        }
        string[] skipProps = ["Id", "ReplyId", "CommenterId", "Commenter", "CreatedAt"];
        PropertyCopier.CopyProperties<Comment>(comment, oldComment, skipProps);
        var status = await SaveAsync();
        return status ? oldComment : null;
    }
}
