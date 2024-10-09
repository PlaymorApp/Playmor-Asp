using Microsoft.EntityFrameworkCore;
using Playmor_Asp.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Playmor_Asp.Domain.Models;

public class Game
{
    public int Id { get; set; }

    [Required]
    public required string Title { get; set; }

    [Required]
    public required string Description { get; set; }

    [Required]
    public required string Details { get; set; }

    [Required]
    public required List<string> Developer { get; set; }

    [Required]
    public required List<string> Publisher { get; set; }

    [Required]
    public required List<string> Platforms { get; set; }

    [Required]
    public required List<string> Genres { get; set; }

    [Required]
    public required List<string> Modes { get; set; }

    [Required]
    public required string Cover { get; set; }

    [Required]
    public required string Artwork { get; set; }

    [Required]
    public required List<ReleaseDate> ReleaseDates { get; set; }

    [Required]
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
    public required WebsiteNames WebsiteName { get; set; }

    public required string WebsiteLink { get; set; }
}
