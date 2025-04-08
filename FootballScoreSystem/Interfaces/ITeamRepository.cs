using FootballScoreSystem.Data.Models;

namespace FootballScoreSystem.Interfaces
{
    public interface ITeamRepository
    {
        Task<IEnumerable<Team>> GetAllTeamsAsync();
        Task<Team> GetTeamByIdAsync(int teamId);
        Task AddTeamAsync(Team team);
        Task UpdateTeamAsync(Team team);
        Task DeleteTeamAsync(int teamId);
    }
}
