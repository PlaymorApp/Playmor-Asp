using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Playmor_Asp.Application.Common.Filters;
using Playmor_Asp.Application.Common.Types;
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

    public async Task<GamePagination> GetPaginatedAsync(int pageNumber, int pageSize, SortByOrder? sortBy, GameFilter? gameFilter)
    {
        var query = _context.Games.AsQueryable();

        // Filter by passed filters
        query = ApplyFilter(query, gameFilter);

        // Sort if applicable
        query = ApplySorting(query, sortBy);

        var totalRecords = await query.CountAsync();

        var totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);

        var games = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

        return new GamePagination { Games = games, TotalRecords = totalRecords, TotalPages = totalPages };
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
        var query = _context.Games.AsQueryable();

        query = sortOrder == SortOrder.desc
            ? query.OrderByDescending(g => g.CreatedAt)
            : query.OrderBy(g => g.CreatedAt);

        return await query.ToListAsync();
    }

    public async Task<ICollection<Game>> GetByReleaseDateAsync(SortOrder sortOrder)
    {
        var query = _context.Games
        .Select(g => new
        {
            Game = g,
            FirstRelease = g.ReleaseDates.Min(rD => rD.Date)
        });

        query = sortOrder == SortOrder.desc
            ? query.OrderByDescending(g => g.FirstRelease)
            : query.OrderBy(g => g.FirstRelease);

        return await query.Select(g => g.Game).ToListAsync();
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

    public async Task<ICollection<string>> GetModesAsync()
    {
        var modes = await _context.Games
        .Select(game => game.Modes)
        .ToListAsync();

        return modes
            .SelectMany(mode => mode)
            .Select(mode => mode.ToLower())
            .Distinct()
            .Select(mode => char.ToUpper(mode[0]) + mode.Substring(1))
            .ToList();
    }

    public async Task<ICollection<string>> GetGenresAsync()
    {
        var genres = await _context.Games
        .Select(game => game.Genres)
        .ToListAsync();

        return genres
            .SelectMany(genre => genre)
            .Select(genre => genre.ToLower())
            .Distinct()
            .Select(genre => char.ToUpper(genre[0]) + genre.Substring(1))
            .ToList();
    }

    public async Task<ICollection<string>> GetPlatformsAsync()
    {
        var platforms = await _context.Games
        .Select(game => game.Platforms)
        .ToListAsync();

        return platforms
            .SelectMany(platform => platform)
            .Select(platform => platform.ToLower())
            .Distinct()
            .Select(platform => char.ToUpper(platform[0]) + platform.Substring(1))
            .ToList();
    }

    public async Task<ICollection<string>> GetDevelopersAsync()
    {
        var developers = await _context.Games
        .Select(game => game.Developer)
        .ToListAsync();

        return developers
            .SelectMany(developer => developer)
            .Select(developer => developer.ToLower())
            .Distinct()
            .Select(developer => char.ToUpper(developer[0]) + developer.Substring(1))
            .ToList();
    }

    public async Task<ICollection<string>> GetPublishersAsync()
    {
        var publishers = await _context.Games
        .Select(game => game.Publisher)
        .ToListAsync();

        return publishers
            .SelectMany(publisher => publisher)
            .Select(publisher => publisher.ToLower())
            .Distinct()
            .Select(publisher => char.ToUpper(publisher[0]) + publisher.Substring(1))
            .ToList();
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

    public IQueryable<Game> ApplyDateRange(IQueryable<Game> query, DateTime? fromDate, DateTime? toDate)
    {
        // Filters according to range [fromDate >= x <= toDate] on earliest release date
        if (fromDate.HasValue && toDate.HasValue)
        {
            query = query.Where(g => g.ReleaseDates.Min(rD => rD.Date) >= fromDate && g.ReleaseDates.Min(rD => rD.Date) <= toDate);
        }
        return query;
    }

    public IQueryable<Game> ApplySorting(IQueryable<Game> query, SortByOrder? sortBy)
    {
        return sortBy switch
        {
            SortByOrder.addedAscending => query.OrderBy(g => g.CreatedAt),
            SortByOrder.addedDescending => query.OrderByDescending(g => g.CreatedAt),
            SortByOrder.releasedAscending => query.OrderBy(g => g.ReleaseDates.Min(rD => rD.Date)),
            SortByOrder.releasedDescending => query.OrderByDescending(g => g.ReleaseDates.Min(rD => rD.Date)),
            _ => query.OrderBy(g => g.Id) // Default sorting by Id
        };
    }

    public IQueryable<Game> ApplyKeyword(IQueryable<Game> query, string? keyword)
    {
        if (!string.IsNullOrEmpty(keyword))
        {
            query = query.Where(g =>
                g.Title.Contains(keyword) ||
                g.Description.Contains(keyword) ||
                g.Developer.Any(dev => dev.Contains(keyword)) ||
                g.Publisher.Any(pub => pub.Contains(keyword)) ||
                g.Genres.Any(genre => genre.Contains(keyword)) ||
                g.Modes.Any(mode => mode.Contains(keyword))
            );
        }

        return query;
    }

    public IQueryable<Game> ApplyGenres(IQueryable<Game> query, ICollection<string>? genres)
    {
        if (!genres.IsNullOrEmpty())
        {
            var lowerGenres = genres.Select(g => g.ToLower()).ToList();
            query = query.Where(g => g.Genres.Any(genre => lowerGenres.Contains(genre.ToLower())));
        }

        return query;
    }

    public IQueryable<Game> ApplyModes(IQueryable<Game> query, ICollection<string>? modes)
    {
        if (!modes.IsNullOrEmpty())
        {
            var lowerModes = modes.Select(g => g.ToLower()).ToList();
            query = query.Where(g => g.Modes.Any(mode => lowerModes.Contains(mode.ToLower())));
        }

        return query;
    }

    public IQueryable<Game> ApplyDevelopers(IQueryable<Game> query, ICollection<string>? developers)
    {
        if (!developers.IsNullOrEmpty())
        {
            var lowerDevelopers = developers.Select(g => g.ToLower()).ToList();
            query = query.Where(g => g.Developer.Any(dev => lowerDevelopers.Contains(dev.ToLower())));
        }

        return query;
    }

    public IQueryable<Game> ApplyPublishers(IQueryable<Game> query, ICollection<string>? publishers)
    {
        if (!publishers.IsNullOrEmpty())
        {
            var lowerPublishers = publishers.Select(g => g.ToLower()).ToList();
            query = query.Where(g => g.Publisher.Any(pub => lowerPublishers.Contains(pub.ToLower())));
        }

        return query;
    }

    public IQueryable<Game> ApplyPlatforms(IQueryable<Game> query, ICollection<string>? platforms)
    {
        if (!platforms.IsNullOrEmpty())
        {
            var lowerPlatforms = platforms.Select(g => g.ToLower()).ToList();
            query = query.Where(g => g.Platforms.Any(platform => lowerPlatforms.Contains(platform.ToLower())));
        }

        return query;
    }

    public IQueryable<Game> ApplyFilter(IQueryable<Game> query, GameFilter? gameFilter)
    {
        query = ApplyDateRange(query, gameFilter?.DateRange?.From, gameFilter?.DateRange?.To);

        query = ApplyKeyword(query, gameFilter?.Keyword);

        query = ApplyGenres(query, gameFilter?.Genres);

        query = ApplyModes(query, gameFilter?.Modes);

        query = ApplyDevelopers(query, gameFilter?.Developers);

        query = ApplyPublishers(query, gameFilter?.Publishers);

        query = ApplyPlatforms(query, gameFilter?.Platforms);

        return query;
    }
}
