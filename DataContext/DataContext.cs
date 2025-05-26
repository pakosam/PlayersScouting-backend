using Microsoft.EntityFrameworkCore;
using PlayersScouting_backend.Entities;

namespace PlayersScouting_backend.Persistence
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<Player> Players { get; set; }
        public DbSet<Ratings> Ratings { get; set; }
        public DbSet<Scout> Scouts { get; set; }
        public DbSet<Stats> Stats { get; set; }
    }
}
