using Xunit;
using Moq;
using BluePathBackend.Controllers;
using BluePath_Backend.Interfaces;
using BluePathBackend.Other;
using BluePath_Backend.Objects;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using BluePathBackend.Interfaces;
using BluePathBackend.Objects;

public class AuthControllerTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<IConfiguration> _configurationMock;
    private readonly Mock<UserManager<ApplicationUser>> _userManagerMock;
    private readonly Mock<JwtTokenGenerator> _jwtTokenGeneratorMock;
    private readonly AuthController _controller;

    public AuthControllerTests()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _configurationMock = new Mock<IConfiguration>();
        _userManagerMock = MockUserManager();
        _jwtTokenGeneratorMock = new Mock<JwtTokenGenerator>(_configurationMock.Object);

        _controller = new AuthController(
            _userRepositoryMock.Object,
            _configurationMock.Object,
            _userManagerMock.Object,
            _jwtTokenGeneratorMock.Object
        );

        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
            new Claim(ClaimTypes.NameIdentifier, "test-user-id")
        }, "mock"));

        _controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext { User = user }
        };
    }

    private Mock<UserManager<ApplicationUser>> MockUserManager()
    {
        var store = new Mock<IUserStore<ApplicationUser>>();
        return new Mock<UserManager<ApplicationUser>>(
            store.Object, null, null, null, null, null, null, null, null
        );
    }

    [Fact]
    public async Task Register_ShouldReturnOk_WhenUserIsRegistered()
    {
        _userRepositoryMock.Setup(r => r.GetByEmailAsync(It.IsAny<string>()))
            .ReturnsAsync((ApplicationUser)null);

        _userRepositoryMock.Setup(r => r.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
            .ReturnsAsync(true);

        var model = new RegisterModel
        {
            Fullname = "Test User",
            Email = "test@example.com",
            Password = "Password123!",
            AccountType = "Standard"
        };

        var result = await _controller.Register(model);

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(200, okResult.StatusCode);
    }

    [Fact]
    public async Task Register_ShouldReturnBadRequest_WhenUserAlreadyExists()
    {
        _userRepositoryMock.Setup(r => r.GetByEmailAsync(It.IsAny<string>()))
            .ReturnsAsync(new ApplicationUser());

        var model = new RegisterModel
        {
            Email = "test@example.com"
        };

        var result = await _controller.Register(model);

        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task Login_ShouldReturnOk_WhenCredentialsAreValid()
    {
        var user = new ApplicationUser { Id = "test-user-id", Email = "test@example.com" };

        _userManagerMock.Setup(u => u.FindByEmailAsync(It.IsAny<string>()))
            .ReturnsAsync(user);

        _userManagerMock.Setup(u => u.CheckPasswordAsync(user, It.IsAny<string>()))
            .ReturnsAsync(true);

        _jwtTokenGeneratorMock.Setup(g => g.GenerateToken(user))
            .Returns("fake-jwt-token");

        var model = new LoginModel
        {
            Email = "test@example.com",
            Password = "Password123!"
        };

        var result = await _controller.Login(model);

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.NotNull(okResult.Value);
    }

    [Fact]
    public async Task Login_ShouldReturnUnauthorized_WhenUserNotFound()
    {
        _userManagerMock.Setup(u => u.FindByEmailAsync(It.IsAny<string>()))
            .ReturnsAsync((ApplicationUser)null);

        var model = new LoginModel
        {
            Email = "wrong@example.com",
            Password = "Password123!"
        };

        var result = await _controller.Login(model);

        Assert.IsType<UnauthorizedObjectResult>(result);
    }

    [Fact]
    public async Task Login_ShouldReturnUnauthorized_WhenPasswordInvalid()
    {
        var user = new ApplicationUser { Id = "test-user-id", Email = "test@example.com" };

        _userManagerMock.Setup(u => u.FindByEmailAsync(It.IsAny<string>()))
            .ReturnsAsync(user);

        _userManagerMock.Setup(u => u.CheckPasswordAsync(user, It.IsAny<string>()))
            .ReturnsAsync(false);

        var model = new LoginModel
        {
            Email = "test@example.com",
            Password = "WrongPassword"
        };

        var result = await _controller.Login(model);

        Assert.IsType<UnauthorizedObjectResult>(result);
    }

    [Fact]
    public void TestAuth_ShouldReturnOk_WhenUserIsAuthenticated()
    {
        var result = _controller.TestAuth();

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.NotNull(okResult.Value);
    }
}
