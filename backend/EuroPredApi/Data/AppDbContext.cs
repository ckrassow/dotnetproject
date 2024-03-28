using EuroPredApi.Models;
using Microsoft.EntityFrameworkCore;

namespace EuroPredApi.Data;

public class AppDbContext: DbContext {

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        {}

        public DbSet<Player> Players { get; set; }
        public DbSet<NationalTeam> NationalTeams { get; set; }
}