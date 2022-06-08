using Microsoft.EntityFrameworkCore;
using PlatformApp.Models;
using PlatformApp.Models.GameLibrary;

namespace PlatformApp.Repository
{
    public class PlatformDbContext : DbContext
    {
        public PlatformDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Platform>().HasKey(e => e.Id);
            modelBuilder.Entity<Platform>().Property(x => x.Name).IsRequired();
            modelBuilder.Entity<Platform>().Property(x => x.Company).IsRequired();

            modelBuilder.Entity<PlatformType>().HasKey(e => e.Id);
            modelBuilder.Entity<PlatformType>().Property(x => x.Description).IsRequired();

            modelBuilder.Entity<Platform>()
                .HasOne(x => x.platformType)
                .WithMany(x => x.Platforms)
                .HasForeignKey(x => x.PlatformTypeId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Game>().HasKey(e => e.Id);
            modelBuilder.Entity<Game>().Property(x => x.JogoId).IsRequired();
            modelBuilder.Entity<Game>().Property(x => x.Name).IsRequired();

            modelBuilder.Entity<PlatformGame>().HasKey(x => new { x.GameId, x.PlatformId });

            modelBuilder.Entity<PlatformGame>()
                .HasOne(x => x.Platform)
                .WithMany(x => x.Games)
                .HasForeignKey(x => x.PlatformId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            modelBuilder.Entity<PlatformGame>()
                .HasOne(x => x.Game)
                .WithMany(x => x.Platforms)
                .HasForeignKey(x => x.GameId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            modelBuilder.Entity<PlatformType>().HasIndex(x => x.Description).IsUnique();
        }

        public DbSet<Platform> platforms { get; set; } = null!;
        public DbSet<PlatformType> platformsType { get; set; } = null!;
        public DbSet<Game> games { get; set; } = null!;
        public DbSet<PlatformGame> platformGame { get; set; } = null!;
    }
}
