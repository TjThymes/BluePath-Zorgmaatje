using Xunit;
using Moq;
using BluePathBackend.Controllers;
using BluePath_Backend.Interfaces;
using BluePath_Backend.Objects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

public class AvatarControllerTests
{
    private readonly Mock<IUserAvatarRepository> _repoMock;
    private readonly AvatarController _controller;

    public AvatarControllerTests()
    {
        _repoMock = new Mock<IUserAvatarRepository>();
        _controller = new AvatarController(_repoMock.Object);

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
    public async Task SetAvatar_ShouldReturnOk_WhenValid()
    {
        _repoMock.Setup(r => r.SetOrUpdateAsync(It.IsAny<string>(), It.IsAny<UserAvatar>()))
            .ReturnsAsync(new UserAvatar());

        var result = await _controller.SetAvatar(new UserAvatar());

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.NotNull(okResult.Value);
    }

    [Fact]
    public async Task GetMyAvatar_ShouldReturnOk_WhenAvatarExists()
    {
        _repoMock.Setup(r => r.GetByUserIdAsync("test-user-id"))
            .ReturnsAsync(new UserAvatar());

        var result = await _controller.GetMyAvatar();

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.NotNull(okResult.Value);
    }
}
