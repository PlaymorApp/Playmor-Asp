using Microsoft.EntityFrameworkCore;
using Playmor_Asp.Data;
using Playmor_Asp.Interfaces;
using Playmor_Asp.Models;

namespace Playmor_Asp.Repositories
{
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
}
