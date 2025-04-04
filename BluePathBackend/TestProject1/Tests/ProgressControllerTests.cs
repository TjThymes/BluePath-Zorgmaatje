using Xunit;
using Moq;
using BluePathBackend.Controllers;
using BluePath_Backend.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using System;

public class ProgressControllerTests
{
    private readonly Mock<IUserRouteProgressRepository> _repoMock;
    private readonly ProgressController _controller;

    public ProgressControllerTests()
    {
        _repoMock = new Mock<IUserRouteProgressRepository>();
        _controller = new ProgressController(_repoMock.Object);

        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
            new Claim(ClaimTypes.NameIdentifier, "test-user-id")
        }, "mock"));

        _controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext { User = user }
        };
    }

    [Fact]
    public async Task Update_ShouldReturnOk_WhenProgressUpdated()
    {
        _repoMock.Setup(r => r.UpdateProgressAsync("test-user-id", It.IsAny<Guid>(), It.IsAny<string>()))
            .ReturnsAsync(new BluePath_Backend.Objects.UserRouteStepProgress());

        var result = await _controller.Update(Guid.NewGuid(), "completed");

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task GetMine_ShouldReturnOk_WhenProgressFound()
    {
        _repoMock.Setup(r => r.GetProgressForUserAsync("test-user-id"))
            .ReturnsAsync(new List<BluePath_Backend.Objects.UserRouteStepProgress>());

        var result = await _controller.GetMine();

        Assert.IsType<OkObjectResult>(result);
    }
}
