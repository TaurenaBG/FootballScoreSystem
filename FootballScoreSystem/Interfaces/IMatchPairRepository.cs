using FootballScoreSystem.Data.Models;

namespace FootballScoreSystem.Interfaces
{
    public interface IMatchPairRepository
    {
        Task AddMatchPairAsync(MatchPair matchPair);  
        Task<MatchPair> GetMatchPairByIdAsync(int id);  
        Task<IEnumerable<MatchPair>> GetAllMatchPairsAsync();  
        Task UpdateMatchPairAsync(MatchPair matchPair);  
        Task DeleteMatchPairAsync(int id);
        
    }
}
