using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class NetScoreDbContext : DbContext
    {
        public NetScoreDbContext()
        {
        }

        public NetScoreDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public virtual DbSet<Team> Teams { get; set; }

        public virtual DbSet<Player> Players { get; set; }

        public virtual DbSet<Fixture> Fixtures { get; set; }

        public virtual DbSet<Tournament> Tournaments { get; set; }

        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<PlayerMatchStatistic> PlayerMatchStatistics { get; set; }

        public virtual DbSet<TeamMatchStatistic> TeamMatchStatistics { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(DbConfiguration.ConnectionString);
            }

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Team>(e =>
            {
                e.HasMany(t => t.Players)
                .WithOne(p => p.Team)
                .IsRequired(false);
            });

            modelBuilder.Entity<Fixture>(e =>
            {
                e.HasOne(f => f.AwayTeam)
                .WithMany(t => t.AwayFixtures)
                .HasForeignKey(f => f.AwayTeamId)
                .OnDelete(DeleteBehavior.Restrict);

                e.HasOne(f => f.HomeTeam)
                .WithMany(t => t.HomeFixtures)
                .HasForeignKey(f => f.HomeTeamId)
                .OnDelete(DeleteBehavior.Restrict);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
