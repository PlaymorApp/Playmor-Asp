using Microsoft.EntityFrameworkCore;
using Playmor_Asp.Domain.Models;

namespace Playmor_Asp.Infrastructure.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }

    public DbSet<Game> Games { get; set; }
    public DbSet<User> Users { get; set; }
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
