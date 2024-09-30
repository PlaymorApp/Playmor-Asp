using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Playmor_Asp.Models
{
    public class Game
    {
        public int Id { get; set; }
        [Required]
        public required string Title { get; set; }
        [Required]
        public required string Description { get; set; }
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
        
    }
    [Owned]
    public class ReleaseDate
    {
        public required string Platform { get; set; }
        public required DateTime Date { get; set; }
    }
}
