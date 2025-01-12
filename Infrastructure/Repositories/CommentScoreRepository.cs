using Microsoft.EntityFrameworkCore;
using Playmor_Asp.Application.Interfaces;
using Playmor_Asp.Domain.Models;
using Playmor_Asp.Infrastructure.Data;

namespace Playmor_Asp.Infrastructure.Repositories;

public class CommentScoreRepository : ICommentScoreRepository
{
    private readonly DataContext _dataContext;
    public CommentScoreRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<CommentScore?> CreateAsync(CommentScore commentScore)
    {
        var newCommentScore = (await _dataContext.CommentScores.AddAsync(commentScore)).Entity;

        if (newCommentScore == null)
        {
            return null;
        }

        var status = await SaveAsync();

        return status ? newCommentScore : null;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var existingCommentScore = await GetByIdAsync(id);

        if (existingCommentScore == null)
        {
            return false;
        }

        _dataContext.Remove(existingCommentScore);

        return await SaveAsync();
    }

    public async Task<ICollection<CommentScore>> GetAllAsync()
    {
        return await _dataContext.CommentScores.ToListAsync();
    }

    public async Task<ICollection<CommentScore>> GetByCommentIdAsync(int id)
    {
        return await _dataContext.CommentScores.Where(cS => cS.CommentId == id).ToListAsync();
    }

    public async Task<CommentScore?> GetByIdAsync(int id)
    {
        return await _dataContext.CommentScores.FindAsync(id);
    }

    public async Task<bool> SaveAsync()
    {
        var entriesWritten = await _dataContext.SaveChangesAsync();
        return entriesWritten > 0;
    }
}
