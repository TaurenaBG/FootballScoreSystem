
using FootballScoreSystem.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace FootballScoreSystem.Data
{
    public class FootballDbContext : DbContext
    {
        public FootballDbContext(DbContextOptions<FootballDbContext> options)
            : base(options)
        {

        }

       
        public DbSet<Team> Teams { get; set; }
        public DbSet<MatchPair> MatchPairs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            
            modelBuilder.Entity<MatchPair>()
                .HasOne(mp => mp.HomeTeam)  
                .WithMany()  
                .HasForeignKey(mp => mp.HomeTeamId)
                .OnDelete(DeleteBehavior.Cascade);  

            modelBuilder.Entity<MatchPair>()
                .HasOne(mp => mp.AwayTeam) 
                .WithMany()  
                .HasForeignKey(mp => mp.AwayTeamId)
                .OnDelete(DeleteBehavior.Cascade); 
        }
    }
}
