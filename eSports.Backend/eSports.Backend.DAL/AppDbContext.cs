using Microsoft.EntityFrameworkCore;
using Players.Domain.Entity;
using Stats.Domain.Entity;
using Teams.Domain.Entity;
using Tournament.Domain.Entity;

namespace eSports.Backend.DAL
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
