using FootballScoreSystem.Data.Models;

namespace FootballScoreSystem.Interfaces
{
    public interface ITeamService
    {
        Task<IEnumerable<Team>> GetAllTeamsAsync();
        Task<Team> GetTeamByIdAsync(int teamId);
        Task CreateTeamAsync(Team team);
        Task UpdateTeamAsync(Team team);
        Task DeleteTeamAsync(int teamId);
        Task<IEnumerable<Team>> GetRankingsAsync();
       
    }
}
