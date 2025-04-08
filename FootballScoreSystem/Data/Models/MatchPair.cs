using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FootballScoreSystem.Data.Models
{
    public class MatchPair
    {
        
        public int MatchPairId { get; set; }

        [ForeignKey("HomeTeam")]
        public int HomeTeamId { get; set; }

        [ForeignKey("AwayTeam")]
        public int AwayTeamId { get; set; }
        public int HomeTeamScore { get; set; }  
        public int AwayTeamScore { get; set; }  

        public Team ?HomeTeam { get; set; }
        public Team ?AwayTeam { get; set; }

        public MatchResult GetResult()
        {
            if (HomeTeamScore > AwayTeamScore)
            {
                return MatchResult.HomeWin;
            }
            else if (HomeTeamScore < AwayTeamScore)
            {
                return MatchResult.AwayWin;
            }
            else
            {
                return MatchResult.Draw;
            }
        }
    }

    public enum MatchResult
    {
        HomeWin = 3,
        AwayWin = 3,
        Draw = 1
    }

}
