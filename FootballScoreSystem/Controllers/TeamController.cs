using FootballScoreSystem.Data.Models;
using FootballScoreSystem.Interfaces;
using FootballScoreSystem.Repository;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class TeamController : ControllerBase
{
    private readonly ITeamService _teamService;

    public TeamController(ITeamService teamService)
    {
        _teamService = teamService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Team>>> GetTeams()
    {
        var teams = await _teamService.GetAllTeamsAsync();
        return Ok(teams);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Team>> GetTeam(int id)
    {
        var team = await _teamService.GetTeamByIdAsync(id);
        if (team == null)
        {
            return NotFound();
        }
        return Ok(team);
    }

    [HttpPost]
    public async Task<ActionResult> CreateTeam([FromBody] Team team)
    {
        await _teamService.CreateTeamAsync(team);
        return CreatedAtAction(nameof(GetTeam), new { id = team.TeamId }, team);
    }

    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTeam(int id, [FromBody] Team team)
    {
        if (id != team.TeamId)
        {
            return BadRequest("ID mismatch.");
        }

        try
        {
            
            await _teamService.UpdateTeamAsync(team);
        }
        catch (ArgumentException)
        {
           
            return NotFound("Team not found.");
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteTeam(int id)
    {
        await _teamService.DeleteTeamAsync(id);
        return NoContent();
    }
}
