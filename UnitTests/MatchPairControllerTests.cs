using Moq;
using FootballScoreSystem.Interfaces;
using FootballScoreSystem.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using FootballScoreSystem.Data.DTO;

namespace FootballScoreSystem.UnitTests;
public class MatchPairControllerTests
{
    private readonly Mock<IMatchPairService> _mockMatchPairService;
    private readonly Mock<ITeamRepository> _mockTeamRepository;
    private readonly MatchPairController _controller;

    public MatchPairControllerTests()
    {
        _mockMatchPairService = new Mock<IMatchPairService>();
        _mockTeamRepository = new Mock<ITeamRepository>();
        _controller = new MatchPairController(_mockMatchPairService.Object, _mockTeamRepository.Object);
    }

    [Fact]
    public async Task CreateMatch_ReturnsBadRequest_WhenMatchPairIsNull()
    {
        // Arrange
        MatchPair matchPair = null;

        // Act
        var result = await _controller.CreateMatch(matchPair);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task CreateMatch_ReturnsBadRequest_WhenTeamsDoNotExist()
    {
        // Arrange
        var matchPair = new MatchPair
        {
            HomeTeamId = 1,
            AwayTeamId = 2
        };


        _mockTeamRepository.Setup(repo => repo.GetTeamByIdAsync(1)).Returns(Task.FromResult<Team>(null));  // Home team doesn't exist
        _mockTeamRepository.Setup(repo => repo.GetTeamByIdAsync(2)).Returns(Task.FromResult<Team>(null));  // Away team doesn't exist

        // Act
        var result = await _controller.CreateMatch(matchPair);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }
    [Fact]
    public async Task CreateMatch_Returns201Created_WhenMatchIsCreatedSuccessfully()
    {
        // Arrange
        var matchPair = new MatchPair { HomeTeamId = 1, AwayTeamId = 2 };
        var createdMatch = new MatchPairResponse { MatchPairDtoId = 1 };


        _mockMatchPairService.Setup(service => service.CreateMatchAsync(matchPair)).Returns(Task.FromResult(createdMatch));


        _mockTeamRepository.Setup(repo => repo.GetTeamByIdAsync(1)).ReturnsAsync(new Team { TeamId = 1, Name = "Team A" });
        _mockTeamRepository.Setup(repo => repo.GetTeamByIdAsync(2)).ReturnsAsync(new Team { TeamId = 2, Name = "Team B" });

        // Act
        var result = await _controller.CreateMatch(matchPair);

        // Assert
        var createdResult = Assert.IsType<CreatedAtActionResult>(result);  // Assert that the result is of type CreatedAtActionResult
        Assert.Equal(201, createdResult.StatusCode);  // Assert that the status code is 201 (Created)
    }
    [Fact]
    public async Task GetMatchById_ReturnsOkResult_WhenMatchExists()
    {
        // Arrange
        var matchId = 1;
        var matchPair = new MatchPair
        {
            MatchPairId = matchId,
            HomeTeamId = 1,
            AwayTeamId = 2,

        };


        _mockMatchPairService.Setup(service => service.GetMatchByIdAsync(matchId)).ReturnsAsync(matchPair);

        // Act
        var result = await _controller.GetMatchById(matchId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<MatchPair>(okResult.Value);
        Assert.Equal(matchId, returnValue.MatchPairId);





    }
    [Fact]
    public async Task GetMatchById_ReturnsNotFound_WhenMatchDoesNotExist()
    {
        // Arrange
        var matchId = 999;  
        _mockMatchPairService.Setup(service => service.GetMatchByIdAsync(matchId)).ReturnsAsync((MatchPair)null);

        // Act
        var result = await _controller.GetMatchById(matchId);

        // Assert
        Assert.IsType<NotFoundResult>(result);  
    }

    [Fact]
    public async Task DeleteMatch_ReturnsNoContent_WhenMatchIsDeletedSuccessfully()
    {
        // Arrange
        var matchId = 1;  
        _mockMatchPairService.Setup(service => service.DeleteMatchAsync(matchId)).Returns(Task.CompletedTask);

        // Act
        var result = await _controller.DeleteMatch(matchId);

        // Assert
        Assert.IsType<NoContentResult>(result);  
    }

}
