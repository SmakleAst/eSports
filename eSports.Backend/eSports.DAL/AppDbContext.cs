using eSports.Domain.Players.Entity;
using eSports.Domain.Stats.Entity;
using eSports.Domain.Teams.Entity;
using eSports.Domain.Tournament.Entity;
using Microsoft.EntityFrameworkCore;

namespace eSports.DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<PlayerEntity> Players { get; set; }
        public DbSet<StatsEntity> Stats { get; set; }
        public DbSet<TeamEntity> Teams { get; set; }
        public DbSet<TournamentEntity> Tournaments { get; set; }
    }
}
