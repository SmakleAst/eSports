using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace eSports.Backend.DAL
{
    public class AppDbContext
    {
        public class AppDbContext : DbContext
        {
            public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
            {
                Database.EnsureCreated();
            }

            public DbSet<PlayerEntity> Players { get; set; }
            public DbSet<StatsEntity> Stats { get; set; }
            public DbSet<TeamsEntity> Teams { get; set; }
            public DbSet<TournamentsEntity> Tournaments { get; set; }
        }
    }
}
