using Dota2Analytics.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Dota2Analytics.Data
{
    public class DotaContext : DbContext
    {
        public DbSet<Hero> Heroes { get; set; }
        public DbSet<HeroStats> HeroStats { get; set; }
        public DbSet<Iteam> Iteams { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<MatchEvent> MatchEvents { get; set; }
        public DbSet<MatchPlayer> MatchPlayers { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DotaContext(DbContextOptions<DotaContext> options)
            : base((options)) { }
    }
}
