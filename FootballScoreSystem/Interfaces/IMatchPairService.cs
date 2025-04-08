using FootballScoreSystem.Data.DTO;
using FootballScoreSystem.Data.Models;

namespace FootballScoreSystem.Interfaces
{
    public interface IMatchPairService
    {
        Task<MatchPairResponse> CreateMatchAsync(MatchPair matchPair); 
        Task<MatchPair> GetMatchByIdAsync(int id);  
        Task<IEnumerable<MatchPair>> GetAllMatchesAsync();  
        Task UpdateMatchAsync(MatchPair matchPair); 
        Task DeleteMatchAsync(int id); 
    }
}
