using Microsoft.EntityFrameworkCore;
using Playmor_Asp.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Playmor_Asp.Domain.Models;

public class Game
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public required string Details { get; set; }
    public required List<string> Developer { get; set; }
    public required List<string> Publisher { get; set; }
    public required List<string> Platforms { get; set; }
    public required List<string> Genres { get; set; }
    public required List<string> Modes { get; set; }
    public required string Cover { get; set; }
    public required string Artwork { get; set; }
    public required List<ReleaseDate> ReleaseDates { get; set; }
    public required List<Website> WebsiteLinks { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}

[Owned]
public class ReleaseDate
{
    public required string Platform { get; set; }
    public required DateTime Date { get; set; }
}

[Owned]
public class Website
{
    public required WebsiteName WebsiteName { get; set; }
    public required string WebsiteLink { get; set; }
}
