using FootballScoreSystem.Data.DTO;
using FootballScoreSystem.Data.Models;
using FootballScoreSystem.Interfaces;

namespace FootballScoreSystem.Services
{
    public class MatchPairService : IMatchPairService
    {
        private readonly IMatchPairRepository _matchPairRepository;
        private readonly ITeamRepository _teamRepository;

        public MatchPairService(IMatchPairRepository matchPairRepository, ITeamRepository teamRepository)
        {
            _matchPairRepository = matchPairRepository;
            _teamRepository = teamRepository;
        }
        public async Task<IEnumerable<MatchPair>> GetAllMatchPairsAsync()
        {
            return await _matchPairRepository.GetAllMatchPairsAsync();
        }

        public async Task<MatchPairResponse> CreateMatchAsync(MatchPair matchPair)
        {
            if (matchPair == null)
                throw new ArgumentNullException(nameof(matchPair));

            
            var homeTeam = await _teamRepository.GetTeamByIdAsync(matchPair.HomeTeamId);
            var awayTeam = await _teamRepository.GetTeamByIdAsync(matchPair.AwayTeamId);

            if (homeTeam == null || awayTeam == null)
            {
                throw new Exception("One or both teams do not exist.");
            }


            matchPair.HomeTeam = homeTeam;  
            matchPair.AwayTeam = awayTeam;  

            
            await _matchPairRepository.AddMatchPairAsync(matchPair);

            
            await UpdateTeamRankingsAsync(matchPair);

            
            var response = new MatchPairResponse
            {
                MatchPairDtoId = matchPair.MatchPairId,  
                HomeTeamId = matchPair.HomeTeamId,
                AwayTeamId = matchPair.AwayTeamId,
                HomeTeamScore = matchPair.HomeTeamScore,
                AwayTeamScore = matchPair.AwayTeamScore
            };

            return response; 
        }


        public async Task<MatchPair> GetMatchByIdAsync(int id)
        {
            return await _matchPairRepository.GetMatchPairByIdAsync(id);
        }

        public async Task<IEnumerable<MatchPair>> GetAllMatchesAsync()
        {
            return await _matchPairRepository.GetAllMatchPairsAsync();
        }

        public async Task UpdateMatchAsync(MatchPair matchPair)
        {
            await _matchPairRepository.UpdateMatchPairAsync(matchPair);
            await UpdateTeamRankingsAsync(matchPair);
        }

        public async Task DeleteMatchAsync(int id)
        {
            await _matchPairRepository.DeleteMatchPairAsync(id);
        }

        private async Task UpdateTeamRankingsAsync(MatchPair matchPair)
        {
            var homeTeam = await _teamRepository.GetTeamByIdAsync(matchPair.HomeTeamId);
            var awayTeam = await _teamRepository.GetTeamByIdAsync(matchPair.AwayTeamId);

            var result = matchPair.GetResult();

            if (result == MatchResult.HomeWin)
            {
                homeTeam.Points += 3;
            }
            else if (result == MatchResult.AwayWin)
            {
                awayTeam.Points += 3;
            }
            else if (result == MatchResult.Draw)
            {
                homeTeam.Points += 1;
                awayTeam.Points += 1;
            }

            homeTeam.PlayedMatches += 1;
            awayTeam.PlayedMatches += 1;

            await _teamRepository.UpdateTeamAsync(homeTeam);
            await _teamRepository.UpdateTeamAsync(awayTeam);
        }
    }

}
