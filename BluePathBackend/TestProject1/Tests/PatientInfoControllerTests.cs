using Xunit;
using Moq;
using BluePathBackend.Controllers;
using BluePath_Backend.Interfaces;
using BluePath_Backend.Objects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using System.Threading.Tasks;
using BluePathBackend.Objects;

public class PatientInfoControllerTests
{
    private readonly Mock<IPatientInfoRepository> _repoMock;
    private readonly Mock<ILogger<PatientInfoController>> _loggerMock;
    private readonly PatientInfoController _controller;

    public PatientInfoControllerTests()
    {
        _repoMock = new Mock<IPatientInfoRepository>();
        _loggerMock = new Mock<ILogger<PatientInfoController>>();
        _controller = new PatientInfoController(_repoMock.Object, _loggerMock.Object);

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
    public async Task Create_ShouldReturnOk_WhenInfoSaved()
    {
        _repoMock.Setup(r => r.CreateAsync(It.IsAny<PatientInfo>()))
            .ReturnsAsync(new PatientInfo());

        var result = await _controller.Create(new PatientInfo());

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task GetMyInfo_ShouldReturnOk_WhenFound()
    {
        _repoMock.Setup(r => r.GetByUserIdAsync("test-user-id"))
            .ReturnsAsync(new PatientInfo());

        var result = await _controller.GetMyInfo();

        Assert.IsType<OkObjectResult>(result);
    }
}
