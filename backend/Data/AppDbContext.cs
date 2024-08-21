using EuroPredApi.Models;
using EuroPredApi.Utils;
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
        public DbSet<NationalTeamPrediction> TeamPredictions { get; set; }
        public DbSet<TournamentPrediction> TournamentPredictions { get; set;}
        public DbSet<PredictionTeam> PredictionTeams { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<UserPrediction<PlayerPrediction>> UserPlayerPredictions { get; set; }
        public DbSet<UserPrediction<NationalTeamPrediction>> UserTeamPredictions { get; set; }
        public DbSet<UserPrediction<TournamentPrediction>> UserTournamentPredictions { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Competition> Competitions { get; set; }
        public DbSet<Score> Scores { get; set; }
        public DbSet<FullTime> FullTimes { get; set; }
        public DbSet<HalfTime> HalfTimes { get; set; }
        public DbSet<GamePrediction> GamePredictions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<UserPrediction<PlayerPrediction>>()
                .Property(e => e.PredictionTypeString)
                .HasConversion<string>();

            modelBuilder.Entity<UserPrediction<NationalTeamPrediction>>()
                .Property(e => e.PredictionTypeString)
                .HasConversion<string>();

            modelBuilder.Entity<UserPrediction<TournamentPrediction>>()
                .Property(e => e.PredictionTypeString)
                .HasConversion<string>();
            
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Author)
                .WithMany(u => u.CommentsWritten)
                .HasForeignKey(c => c.AuthorId)
                .OnDelete(DeleteBehavior.Cascade);
            
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.User)
                .WithMany(u => u.CommentsReceived)
                .HasForeignKey(c => c.RecipientId)
                .OnDelete(DeleteBehavior.Cascade);
            
            modelBuilder.Entity<GamePrediction>()
                .HasKey(gp => new { gp.UserId, gp.GameId });
        }

}