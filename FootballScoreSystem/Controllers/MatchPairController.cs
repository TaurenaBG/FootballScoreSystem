using FootballScoreSystem.Data.Models;
using FootballScoreSystem.Interfaces;
using FootballScoreSystem.Repository;
using FootballScoreSystem.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class MatchPairController : ControllerBase
{
    private readonly IMatchPairService _matchPairService;
    private readonly ITeamRepository _teamRepository;

    public MatchPairController(IMatchPairService matchPairService, ITeamRepository teamRepository)
    {
        _matchPairService = matchPairService;
        _teamRepository = teamRepository;
    }


    [HttpPost]
    public async Task<IActionResult> CreateMatch([FromBody] MatchPair matchPair)
    {
        if (matchPair == null)
        {
            return BadRequest("Match pair data is required.");
        }

        try
        {
           
            var homeTeam = await _teamRepository.GetTeamByIdAsync(matchPair.HomeTeamId);
            var awayTeam = await _teamRepository.GetTeamByIdAsync(matchPair.AwayTeamId);

            if (homeTeam == null || awayTeam == null)
            {
                return BadRequest("One or both teams do not exist.");
            }

            
            var result = await _matchPairService.CreateMatchAsync(matchPair);

            
            return CreatedAtAction(nameof(GetMatchById), new { id = result.MatchPairDtoId }, result);
        }
        catch (Exception ex)
        {
            
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }





    [HttpGet("{id}")]
    public async Task<IActionResult> GetMatchById(int id)
    {
        var matchPair = await _matchPairService.GetMatchByIdAsync(id);
        if (matchPair == null)
        {
            return NotFound();
        }

        return Ok(matchPair);
    }

   
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMatch(int id)
    {
        await _matchPairService.DeleteMatchAsync(id);
        return NoContent();
    }
}

