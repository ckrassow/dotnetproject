using EuroPredApi.Models;
using Microsoft.EntityFrameworkCore;

namespace EuroPredApi.Data;

public class AppDbContext: DbContext {

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        {}

        public DbSet<Player> Players { get; set; }
        public DbSet<NationalTeam> NationalTeams { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<PlayerPrediction> PlayerPredictions { get; set; }
        public DbSet<TeamPrediction> TeamPredictions { get; set; }
        public DbSet<TournamentPrediction> TournamentPredictions { get; set;}
        public DbSet<Team> Teams { get; set; }
        public DbSet<UserPrediction<PlayerPrediction>> UserPlayerPredictions { get; set; }
        public DbSet<UserPrediction<TeamPrediction>> UserTeamPredictions { get; set; }
        public DbSet<UserPrediction<TournamentPrediction>> UserTournamentPredictions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<UserPrediction<PlayerPrediction>>()
                .Property(e => e.PredictionTypeString)
                .HasConversion<string>();

            modelBuilder.Entity<UserPrediction<TeamPrediction>>()
                .Property(e => e.PredictionTypeString)
                .HasConversion<string>();

            modelBuilder.Entity<UserPrediction<TournamentPrediction>>()
                .Property(e => e.PredictionTypeString)
                .HasConversion<string>();
        }

}