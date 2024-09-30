using Playmor_Asp.Models;

namespace Playmor_Asp.Interfaces
{
    public interface IGameService
    {
        Game? GetGame(int id);
        bool CreateGame(Game game);
        bool UpdateGame(int id, Game game);
        bool DeleteGame(int id);
        ICollection<Game> GetGames();
        ICollection<Game> GetGamesByTitle(string title);
        ICollection<Game> GetGamesByKeyword(string keyword);
        ICollection<Game> GetGamesByModes(ICollection<string> modes);
        ICollection<Game> GetGamesByGenres(ICollection<string> genres);
    }
}
