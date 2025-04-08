using Moq;
using FootballScoreSystem.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using FootballScoreSystem.Data.Models;

namespace FootballScoreSystem.UnitTests
{
    public class RankingControllerTests
    {
        private readonly Mock<ITeamService> _mockTeamService;
        private readonly RankingController _controller;

        public RankingControllerTests()
        {
            _mockTeamService = new Mock<ITeamService>();
            _controller = new RankingController(_mockTeamService.Object);
        }

        [Fact]
        public async Task GetRankings_ReturnsOk_WhenRankingsAreFetchedSuccessfully()
        {
            // Arrange
            var teams = new List<Team>
            {
             new Team { TeamId = 1, Name = "Team A", Points = 10 },
             new Team { TeamId = 2, Name = "Team B", Points = 8 },
             new Team { TeamId = 3, Name = "Team C", Points = 5 }
            };
            _mockTeamService.Setup(service => service.GetRankingsAsync()).ReturnsAsync(teams); 

            // Act
            var result = await _controller.GetRankings(); 

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result); 
            Assert.Equal(200, okResult.StatusCode); 
            var returnValue = Assert.IsAssignableFrom<List<Team>>(okResult.Value); 
            Assert.Equal(teams, returnValue); 
        }


        
    }
}
