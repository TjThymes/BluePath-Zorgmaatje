using Xunit;
using Moq;
using BluePathBackend.Controllers;
using BluePath_Backend.Interfaces;
using BluePath_Backend.Objects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;

public class RouteControllerTests
{
    private readonly Mock<IRouteRepository> _repoMock;
    private readonly RouteController _controller;

    public RouteControllerTests()
    {
        _repoMock = new Mock<IRouteRepository>();
        _controller = new RouteController(_repoMock.Object);

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
    public async Task GetSteps_ShouldReturnOk_WhenValidRouteType()
    {
        _repoMock.Setup(r => r.GetStepsForRouteAsync("A"))
            .ReturnsAsync(new List<RouteStep>());

        var result = await _controller.GetSteps("A");

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task GetSteps_ShouldReturnBadRequest_WhenInvalidRouteType()
    {
        var result = await _controller.GetSteps("X");

        Assert.IsType<BadRequestObjectResult>(result);
    }
}
