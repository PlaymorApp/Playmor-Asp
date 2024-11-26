using Microsoft.EntityFrameworkCore;
using Playmor_Asp.Domain.Models;

namespace Playmor_Asp.Infrastructure.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }

    public DbSet<Game> Games { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserGame> UserGames { get; set; }
    public DbSet<Message> Messages { get; set; }
    public DbSet<Comment> Comments { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Game>()
            .OwnsMany(g => g.ReleaseDates);

        modelBuilder.Entity<Game>()
            .OwnsMany(g => g.WebsiteLinks, wb =>
            {
                wb.Property(w => w.WebsiteName).HasConversion<string>();
            });

    }
}
