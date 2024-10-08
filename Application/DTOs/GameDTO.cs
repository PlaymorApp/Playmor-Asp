using Playmor_Asp.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace Playmor_Asp.Application.DTOs
{
    public class GameDTO
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
}
