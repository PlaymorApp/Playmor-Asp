using Microsoft.EntityFrameworkCore;
using Playmor_Asp.Application.Interfaces;
using Playmor_Asp.Domain.Enums;
using Playmor_Asp.Domain.Models;
using Playmor_Asp.Infrastructure.Data;

namespace Playmor_Asp.Infrastructure.Repositories;

public class GameRepository : IGameRepository
{
    private readonly DataContext _context;

    public GameRepository(DataContext dataContext)
    {
        _context = dataContext;
    }

    public async Task<Game?> GetAsync(int id)
    {
        return await _context.Games.FirstOrDefaultAsync(g => g.Id == id);
    }

    public async Task<ICollection<Game>> GetAllAsync()
    {
        var games = await _context.Games.OrderBy(game => game.Id).ToListAsync();
        return games;
    }

    public async Task<ICollection<Game>> GetPaginatedAsync(int pageNumber, int pageSize)
    {
        return await _context.Games
            .OrderBy(game => game.Id)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<ICollection<Game>> GetByGenresAsync(ICollection<string> genres)
    {
        return await _context.Games.Where(g => g.Genres.Any(genre => genres.Contains(genre.ToLower()))).ToListAsync();
    }

    public async Task<ICollection<Game>> GetByKeywordAsync(string keyword)
    {
        if (string.IsNullOrWhiteSpace(keyword))
        {
            return [];
        }

        var titleMatches = _context.Games.Where(g => g.Title.Contains(keyword));
        var descriptionMatches = _context.Games.Where(g => g.Description.Contains(keyword));
        var developerMatches = _context.Games.Where(g => g.Developer.Any(dev => dev.Contains(keyword)));
        var publisherMatches = _context.Games.Where(g => g.Publisher.Any(pub => pub.Contains(keyword)));
        var genreMatches = _context.Games.Where(g => g.Genres.Any(genre => genre.Contains(keyword)));
        var modeMatches = _context.Games.Where(g => g.Modes.Any(mode => mode.Contains(keyword)));

        return await titleMatches
            .Union(descriptionMatches)
            .Union(developerMatches)
            .Union(publisherMatches)
            .Union(genreMatches)
            .Union(modeMatches)
            .ToListAsync();
    }

    public async Task<ICollection<Game>> GetByAddedDateAsync(SortOrder sortOrder)
    {
        if (sortOrder.ToString() == "desc")
        {
            return await _context.Games.OrderByDescending(g => g.CreatedAt).ToListAsync();
        }
        else
        {
            return await _context.Games.OrderBy(g => g.CreatedAt).ToListAsync();
        }
    }

    public async Task<ICollection<Game>> GetByReleaseDateAsync(SortOrder sortOrder)
    {
        if (sortOrder.ToString() == "desc")
        {
            return await _context.Games
               .Select(g => new
               {
                   Game = g,
                   FirstRelease = g.ReleaseDates.Min(rD => rD.Date)
               })
               .OrderByDescending(g => g.FirstRelease)
               .Select(g => g.Game)
               .ToListAsync();
        }
        else
        {
            return await _context.Games
               .Select(g => new
               {
                   Game = g,
                   FirstRelease = g.ReleaseDates.Min(rD => rD.Date)
               })
               .OrderBy(g => g.FirstRelease)
               .Select(g => g.Game)
               .ToListAsync();
        }
    }

    public async Task<ICollection<Game>> GetByModesAsync(ICollection<string> modes)
    {
        return await _context.Games.Where(g => g.Modes.Any(mode => modes.Contains(mode))).ToListAsync();
    }

    public async Task<ICollection<Game>> GetByTitleAsync(string title)
    {
        return await _context.Games.Where(g => g.Title.Contains(title)).ToListAsync();
    }

    public async Task<bool> CreateAsync(Game game)
    {
        await _context.Games.AddAsync(game);
        return await SaveAsync();
    }

    public async Task<bool> UpdateAsync(int id, Game game)
    {
        var oldGame = await GetAsync(id);
        if (oldGame == null)
        {
            return false;
        }

        CopyProperties(game, oldGame);
        return await SaveAsync();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var oldGame = await GetAsync(id);
        if (oldGame == null)
        {
            return false;
        }

        _context.Remove(oldGame);
        return await SaveAsync();
    }

    public async Task<bool> SaveAsync()
    {
        var entriesWritten = await _context.SaveChangesAsync();
        return entriesWritten > 0;
    }

    public void CopyProperties(Game gameSource, Game gameDestination)
    {
        var properties = typeof(Game).GetProperties().Where(prop => prop.CanRead && prop.CanWrite);
        foreach (var property in properties)
        {
            if (property.Name.Equals("Id", StringComparison.CurrentCultureIgnoreCase)) continue;
            var value = property.GetValue(gameSource, null);
            property.SetValue(gameDestination, value, null);
        }
    }

}
