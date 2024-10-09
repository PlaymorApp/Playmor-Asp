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

    public Game? Get(int id)
    {
        return _context.Games.FirstOrDefault(g => g.Id == id);
    }

    public ICollection<Game> GetAll()
    {
        return _context.Games.OrderBy(game => game.Id).ToList();
    }

    public ICollection<Game> GetByGenres(ICollection<string> genres)
    {
        return _context.Games.Where(g => g.Genres.Any(genre => genres.Contains(genre.ToLower()))).ToList();
    }

    public ICollection<Game> GetByKeyword(string keyword)
    {
        var query = _context.Games.AsQueryable();

        if (string.IsNullOrWhiteSpace(keyword))
        {
            return new List<Game>();
        }

        var titleMatches = query.Where(g => g.Title.Contains(keyword));
        var descriptionMatches = query.Where(g => g.Description.Contains(keyword));
        var developerMatches = query.Where(g => g.Developer.Any(dev => dev.Contains(keyword)));
        var publisherMatches = query.Where(g => g.Publisher.Any(pub => pub.Contains(keyword)));
        var genreMatches = query.Where(g => g.Genres.Any(genre => genre.Contains(keyword)));
        var modeMatches = query.Where(g => g.Modes.Any(mode => mode.Contains(keyword)));

        return titleMatches
            .Union(descriptionMatches)
            .Union(developerMatches)
            .Union(publisherMatches)
            .Union(genreMatches)
            .Union(modeMatches)
            .ToList();
    }

    public ICollection<Game> GetByAddedDate(SortOrder sortOrder)
    {
        if (sortOrder.ToString() != "asc" && sortOrder.ToString() != "desc")
        {
            throw new ArgumentException("Invalid sort order. Use 'asc' or 'desc'.");
        }

        if (sortOrder.ToString() == "desc")
        {
            return _context.Games.OrderByDescending(g => g.CreatedAt).ToList();
        }
        else
        {
            return _context.Games.OrderBy(g => g.CreatedAt).ToList();
        }
    }

    public ICollection<Game> GetByReleaseDate(SortOrder sortOrder)
    {
        if (sortOrder.ToString() != "asc" && sortOrder.ToString() != "desc")
        {
            throw new ArgumentException("Invalid sort order. Use 'asc' or 'desc'.");
        }

        if (sortOrder.ToString() == "desc")
        {
            return _context.Games
               .Select(g => new
               {
                   Game = g,
                   FirstRelease = g.ReleaseDates.Min(rD => rD.Date)
               })
               .OrderByDescending(g => g.FirstRelease)
               .Select(g => g.Game)
               .ToList();
        }
        else
        {
            return _context.Games
               .Select(g => new
               {
                   Game = g,
                   FirstRelease = g.ReleaseDates.Min(rD => rD.Date)
               })
               .OrderBy(g => g.FirstRelease)
               .Select(g => g.Game)
               .ToList();
        }
    }

    public ICollection<Game> GetByModes(ICollection<string> modes)
    {
        return _context.Games.Where(g => g.Modes.Any(mode => modes.Contains(mode))).ToList();
    }

    public ICollection<Game> GetByTitle(string title)
    {
        return _context.Games.Where(g => g.Title.Contains(title)).ToList();
    }

    public bool Create(Game game)
    {
        _context.Games.Add(game);
        return Save();
    }

    public bool Update(int id, Game game)
    {
        var oldGame = Get(id);
        if (oldGame == null)
        {
            return false;
        }

        CopyProperties(game, oldGame);
        return Save();
    }

    public bool Delete(int id)
    {
        var oldGame = Get(id);
        if (oldGame == null)
        {
            return false;
        }

        _context.Remove(oldGame);
        return Save();
    }

    public bool Save()
    {
        var entriesWritten = _context.SaveChanges();
        return entriesWritten > 0;
    }

    public void CopyProperties(Game gameSource, Game gameDestination)
    {
        var properties = typeof(Game).GetProperties().Where(prop => prop.CanRead && prop.CanWrite);
        foreach (var property in properties)
        {
            var value = property.GetValue(gameSource, null);
            property.SetValue(gameDestination, value, null);
        }
    }

}
