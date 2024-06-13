using Microsoft.EntityFrameworkCore;
using GameCollectionTracker.Models;

namespace GameCollectionTracker.Data;

public class GameContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(ConnectionStringHelper.GetConnectionString());
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Game> Games { get; set; }
    public DbSet<GamePlayed> GamesPlayed { get; set; }
    public DbSet<GamePlayer> GamePlayers { get; set; }
    public DbSet<Player> Players { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<GamePlayed>()
        .HasMany(e => e.Players)
        .WithMany(e => e.GamesPlayed)
        .UsingEntity<GamePlayer>();

        modelBuilder.Entity<Player>()
        .HasMany(e => e.GamesPlayed)
        .WithMany(e => e.Players)
        .UsingEntity<GamePlayer>();

        modelBuilder.UseCollation("SQL_Latin1_General_CP1_CS_AS");
    }
}