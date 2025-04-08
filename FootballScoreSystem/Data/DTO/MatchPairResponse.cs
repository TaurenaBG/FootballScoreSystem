namespace FootballScoreSystem.Data.DTO
{
    public class MatchPairResponse
    {
        public int MatchPairDtoId { get; set; }
        public int HomeTeamId { get; set; }
        public int AwayTeamId { get; set; }
        public int HomeTeamScore { get; set; }
        public int AwayTeamScore { get; set; }
    }
}
