using FootballScoreSystem.Data;
using FootballScoreSystem.Data.Models;
using FootballScoreSystem.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;

namespace FootballScoreSystem.Repository
{
    public class MatchPairRepository : IMatchPairRepository
    {
        private readonly FootballDbContext _context;

        public MatchPairRepository(FootballDbContext context)
        {
            _context = context;
        }


        public async Task AddMatchPairAsync(MatchPair matchPair)
        {
            await _context.MatchPairs.AddAsync(matchPair);
            await _context.SaveChangesAsync();
        }

        public async Task<MatchPair> GetMatchPairByIdAsync(int id)
        {
           
            var matchPair = await _context.MatchPairs
                                          .Include(mp => mp.HomeTeam)
                                          .Include(mp => mp.AwayTeam)
                                          .FirstOrDefaultAsync(mp => mp.MatchPairId == id);


            if (matchPair == null)
            {
                throw new ArgumentNullException(nameof(matchPair), "The match does not exist or has been deleted.");
            }


            if (matchPair.HomeTeam == null || matchPair.AwayTeam == null)
            {
                throw new ArgumentNullException(nameof(matchPair), "Not enough information.");
            }

            return matchPair; 
        }


        public async Task<IEnumerable<MatchPair>> GetAllMatchPairsAsync()
        {
            return await _context.MatchPairs
                                 .Include(mp => mp.HomeTeam)
                                 .Include(mp => mp.AwayTeam)
                                 .ToListAsync();
        }

        public async Task UpdateMatchPairAsync(MatchPair matchPair)
        {
            _context.MatchPairs.Update(matchPair);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteMatchPairAsync(int id)
        {
            var matchPair = await GetMatchPairByIdAsync(id);
            if (matchPair != null)
            {
                _context.MatchPairs.Remove(matchPair);
                await _context.SaveChangesAsync();
            }
        }
       
    }

}
