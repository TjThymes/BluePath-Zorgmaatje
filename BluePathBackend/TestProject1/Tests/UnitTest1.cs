using Xunit;
using BluePathBackend.Other;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace BluePathTests.Tests
{
    public class JwtTokenGeneratorTests
    {
        [Fact]
        public void GenerateToken_ShouldReturnToken()
        {
            // Arrange
            var inMemorySettings = new Dictionary<string, string> {
                {"Jwt:Key", "SuperSecretTestKeyThatIsLongEnough1234567890"},
                {"Jwt:Issuer", "TestIssuer"},
                {"Jwt:Audience", "TestAudience"}
            };

            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            var generator = new JwtTokenGenerator(configuration);

            var fakeUser = new IdentityUser
            {
                Id = "user123",
                Email = "test@example.com",
                UserName = "testuser"
            };

            // Act
            var token = generator.GenerateToken(fakeUser);

            // Assert
            Assert.NotNull(token);
            Assert.IsType<string>(token);
        }
    }
}
