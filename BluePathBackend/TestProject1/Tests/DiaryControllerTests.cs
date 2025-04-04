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

public class DiaryControllerTests
{
    private readonly Mock<IDiaryRepository> _repoMock;
    private readonly DiaryController _controller;

    public DiaryControllerTests()
    {
        _repoMock = new Mock<IDiaryRepository>();
        _controller = new DiaryController(_repoMock.Object);

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
    public async Task Create_ShouldReturnOk_WhenEntryCreated()
    {
        _repoMock.Setup(r => r.CreateAsync(It.IsAny<DiaryEntry>()))
            .ReturnsAsync(new DiaryEntry());

        var result = await _controller.Create(new DiaryEntry());

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task GetAll_ShouldReturnOk_WithEntries()
    {
        _repoMock.Setup(r => r.GetAllForUserAsync("test-user-id"))
            .ReturnsAsync(new List<DiaryEntry>());

        var result = await _controller.GetAll();

        Assert.IsType<OkObjectResult>(result);
    }
}
