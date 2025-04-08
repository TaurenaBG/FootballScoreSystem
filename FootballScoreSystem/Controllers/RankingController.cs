using FootballScoreSystem.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class RankingController : ControllerBase
{
    private readonly ITeamService _teamService;

    public RankingController(ITeamService teamService)
    {
        _teamService = teamService;
    }

    
    [HttpGet]
    public async Task<IActionResult> GetRankings()
    {
        var rankings = await _teamService.GetRankingsAsync();
        return Ok(rankings);
    }
}