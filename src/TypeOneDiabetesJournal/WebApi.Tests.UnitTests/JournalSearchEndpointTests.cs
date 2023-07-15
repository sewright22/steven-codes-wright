using AutoFixture.AutoMoq;
using AutoFixture;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Features.UserLogin;
using Microsoft.Extensions.Configuration;
using WebApi.Features.JournalSearch;
using Services;
using FastEndpoints;

namespace WebApi.Tests.UnitTests
{
    [TestClass]
    public class JournalSearchEndpointTests
    {
        [TestMethod]
        public async Task test()
        {
            // Arrange - test set up
            var fixture = new Fixture().Customize(new AutoMoqCustomization());
            var mockConfig = fixture.Freeze<Mock<IConfiguration>>();
            var mockLogger = fixture.Freeze<Mock<ILogger<JournalSearchEndpoint>>>();
            var mockUserService = fixture.Freeze<Mock<IUserService>>();


            // Act
            var endpoint = Factory.Create<JournalSearchEndpoint>();
            var req = new JournalSearchRequest();
            req.SearchValue = "Test";
            var response = await endpoint.ExecuteAsync(req, default);

            // Assert
            Assert.IsNotNull(response);
            Assert.IsFalse(endpoint.ValidationFailed);
            // Assert.IsNotNull(response.Token);
        }

    }
}
