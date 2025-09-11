using Dota2Analytics.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Dota2Analytics.Data
{
    public class DotaContext : DbContext
    {
        public DbSet<Hero> Heroes { get; set; }
        public DbSet<HeroStats> HeroStats { get; set; }
        public DbSet<Iteam> Items { get; set; }
        public DbSet<ItemPurchase> ItemPurchases { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<MatchEvent> MatchEvents { get; set; }
        public DbSet<MatchPlayer> MatchPlayers { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<RequestLog> RequestLogs { get; set; }
        public DotaContext(DbContextOptions<DotaContext> options)
            : base((options)) { }

        /*
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {//оптимизирует запросы и место, выделяемое для таблиц в бд
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new RequestLogConfiguration());//сделать
        }*/

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Properties<Enum>()
                .HaveConversion<int>()
                .HaveColumnType("integer");
        }//все enum в int для sql
    }
}
