using Playmor_Asp.Models;

namespace Playmor_Asp.Interfaces
{
    public interface IGameRepository
    {
        Game? Get(int id);
        ICollection<Game> GetAll();
        ICollection<Game> GetByTitle(string title);
        ICollection<Game> GetByKeyword(string keyword);
        ICollection<Game> GetByModes(ICollection<string> modes);
        ICollection<Game> GetByGenres(ICollection<string> genres);
        public bool Create(Game user);
        public bool Update(int id, Game car);
        public bool Delete(int id);
        public bool Save();
    }
}
