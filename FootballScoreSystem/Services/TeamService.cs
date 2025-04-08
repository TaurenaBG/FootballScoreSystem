using FootballScoreSystem.Data.Models;
using FootballScoreSystem.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FootballScoreSystem.Services
{
    public class TeamService : ITeamService
    {
        private readonly ITeamRepository _teamRepository;
        private readonly IMatchPairService _matchPairService;

        public TeamService(ITeamRepository teamRepository, IMatchPairService matchPairService)
        {
            _teamRepository = teamRepository;
            _matchPairService = matchPairService;
        }

        public async Task<IEnumerable<Team>> GetAllTeamsAsync()
        {
            return await _teamRepository.GetAllTeamsAsync();
        }

        public async Task<Team> GetTeamByIdAsync(int teamId)
        {
            return await _teamRepository.GetTeamByIdAsync(teamId);
        }

        public async Task CreateTeamAsync(Team team)
        {
            await _teamRepository.AddTeamAsync(team);
        }

        public async Task UpdateTeamAsync(Team team)
        {
            await _teamRepository.UpdateTeamAsync(team);
        }


        public async Task DeleteTeamAsync(int teamId)
        {
            await _teamRepository.DeleteTeamAsync(teamId);
        }

        public async Task<IEnumerable<Team>> GetRankingsAsync()
        {
            var teams = await _teamRepository.GetAllTeamsAsync();

            
            var sortedTeams = teams
                .OrderByDescending(t => t.Points)
                .ThenBy(t => t.PlayedMatches)
                .ToList();

            return sortedTeams;
        }
       
    }
}
