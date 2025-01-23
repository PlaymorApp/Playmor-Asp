using Playmor_Asp.Domain.Models;

namespace Playmor_Asp.Application.DTOs.Game;

public class GamePostDTO
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string Details { get; set; }
    public List<string> Developer { get; set; }
    public List<string> Publisher { get; set; }
    public List<string> Platforms { get; set; }
    public List<string> Genres { get; set; }
    public List<string> Modes { get; set; }
    public string Cover { get; set; }
    public string Artwork { get; set; }
    public List<ReleaseDate> ReleaseDates { get; set; }
    public List<Website> WebsiteLinks { get; set; }
}
