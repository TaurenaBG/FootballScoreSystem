using Moq;
using FootballScoreSystem.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using FootballScoreSystem.Interfaces;

namespace FootballScoreSystem.UnitTests
{
    public class TeamControllerTests
    {
        private readonly Mock<ITeamService> _mockTeamService;
        private readonly TeamController _controller;

        public TeamControllerTests()
        {
            
            _mockTeamService = new Mock<ITeamService>();
            _controller = new TeamController(_mockTeamService.Object);
        }

        
        [Fact]
        public async Task GetTeam_ReturnsNotFound_WhenTeamIsNotFound()
        {
            // Arrange
            var teamId = 1;
            _mockTeamService.Setup(service => service.GetTeamByIdAsync(teamId)).ReturnsAsync((Team)null); // Mock that no team is found

            // Act
            var result = await _controller.GetTeam(teamId); 

            // Assert
            var actionResult = Assert.IsType<ActionResult<Team>>(result); 
            var notFoundResult = Assert.IsType<NotFoundResult>(actionResult.Result); 
        }
        [Fact]
        public async Task GetTeam_ReturnsOk_WhenTeamIsFound()
        {
            // Arrange
            var teamId = 1; 
            var expectedTeam = new Team { TeamId = teamId, Name = "Team A" }; 
            _mockTeamService.Setup(service => service.GetTeamByIdAsync(teamId)).ReturnsAsync(expectedTeam); 

            // Act
            var result = await _controller.GetTeam(teamId); 

            // Assert
            var actionResult = Assert.IsType<ActionResult<Team>>(result); 
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result); 
            var returnedTeam = Assert.IsType<Team>(okResult.Value); 
            Assert.Equal(expectedTeam.TeamId, returnedTeam.TeamId); 
            Assert.Equal(expectedTeam.Name, returnedTeam.Name); 
        }
        [Fact]
        public async Task CreateTeam_ReturnsCreatedAtAction_WhenTeamIsCreated()
        {
            // Arrange
            var team = new Team { TeamId = 1, Name = "Team A" }; 
            _mockTeamService.Setup(service => service.CreateTeamAsync(team)).Returns(Task.CompletedTask); 

            // Act
            var result = await _controller.CreateTeam(team); 

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result); 
            Assert.Equal(201, createdResult.StatusCode); 
            Assert.Equal(nameof(_controller.GetTeam), createdResult.ActionName); 
            Assert.Equal(team.TeamId, createdResult.RouteValues["id"]); 
            Assert.Equal(team, createdResult.Value); 
        }

        [Fact]
        public async Task DeleteTeam_ReturnsNoContent_WhenTeamIsDeleted()
        {
            // Arrange
            var teamId = 1; 
            _mockTeamService.Setup(service => service.DeleteTeamAsync(teamId)).Returns(Task.CompletedTask); // Mock the DeleteTeamAsync service call

            // Act
            var result = await _controller.DeleteTeam(teamId); 

            // Assert
            var noContentResult = Assert.IsType<NoContentResult>(result); 
            Assert.Equal(204, noContentResult.StatusCode); 
        }



    }
}
