using FootballScoreSystem.Data;
using FootballScoreSystem.Data.Models;
using FootballScoreSystem.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FootballScoreSystem.Repository
{
    public class TeamRepository : ITeamRepository
    {
        private readonly FootballDbContext _context;

        public TeamRepository(FootballDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Team>> GetAllTeamsAsync()
        {
            return await _context.Teams.ToListAsync();
        }

        public async Task<Team> GetTeamByIdAsync(int teamId)
        {
            return await _context.Teams.FirstOrDefaultAsync(t => t.TeamId == teamId);
        }

        public async Task AddTeamAsync(Team team)
        {
            await _context.Teams.AddAsync(team);
            await _context.SaveChangesAsync();
        }

        
        public async Task UpdateTeamAsync(Team team)
        {
            
            var existingTeam = await _context.Teams.FindAsync(team.TeamId);

            if (existingTeam == null)
            {
                
                throw new ArgumentException("The team does not exist.");
            }

            
            existingTeam.Name = team.Name;
            existingTeam.Points = team.Points;
            existingTeam.PlayedMatches = team.PlayedMatches;

           
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTeamAsync(int teamId)
        {
            var team = await _context.Teams.FindAsync(teamId);
            if (team != null)
            {
                 var matchPairs = await _context.MatchPairs
                 .Where(mp => mp.AwayTeamId == teamId || mp.HomeTeamId == teamId)
                 .ToListAsync();

                _context.MatchPairs.RemoveRange(matchPairs); // Remove related MatchPairs

                _context.Teams.Remove(team);
                await _context.SaveChangesAsync();
            }
        }
    }
}
