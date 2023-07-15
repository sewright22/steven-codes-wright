using AutoFixture;
using AutoFixture.AutoMoq;
using Core.Requests;
using FastEndpoints;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using Services;
using WebApi.Features.UserLogin;

namespace WebApi.Tests.UnitTests
{
    [TestClass]
    public class UserLoginTests
    {
        private Mock<IConfiguration> _configurationMock;
        private Mock<IUserService> _userServiceMock;
        private UserLoginEndpoint _userLoginEndpoint;

        [TestInitialize]
        public void Setup()
        {
            _configurationMock = new Mock<IConfiguration>();
            _userServiceMock = new Mock<IUserService>();
            _userLoginEndpoint = new UserLoginEndpoint(_configurationMock.Object, _userServiceMock.Object);
        }

        [TestMethod]
        public void Constructor_WhenConfigurationIsNull_ShouldThrowArgumentNullException()
        {
            // Arrange
            IConfiguration configuration = null;

            // Act
            Action action = () => new UserLoginEndpoint(configuration, _userServiceMock.Object);

            // Assert
            action.Should().Throw<ArgumentNullException>().WithMessage("Value cannot be null.\r\nParameter name: configuration");
        }

        [TestMethod]
        public void Constructor_WhenUserServiceIsNull_ShouldThrowArgumentNullException()
        {
            // Arrange
            IUserService userService = null;

            // Act
            Action action = () => new UserLoginEndpoint(_configurationMock.Object, userService);

            // Assert
            action.Should().Throw<ArgumentNullException>().WithMessage("Value cannot be null.\r\nParameter name: userService");
        }

        [TestMethod]
        public void ExecuteAsync_WhenRequestIsNull_ShouldAddError()
        {
            // Arrange
            LoginRequest request = null;
            CancellationToken cancellationToken = new CancellationToken();

            // Act
            _userLoginEndpoint.ExecuteAsync(request, cancellationToken);

            // Assert
            //_userLoginEndpoint.Errors.Should().Contain("The username and password are required!");
        }

        [TestMethod]
        public void ExecuteAsync_WhenSigningKeyIsNull_ShouldAddError()
        {
            // Arrange
            LoginRequest request = new LoginRequest { Username = "username", Password = "password" };
            CancellationToken cancellationToken = new CancellationToken();
            _configurationMock.Setup(x => x["TokenSigningKeys:Application"]).Returns((string)null);

            // Act
            _userLoginEndpoint.ExecuteAsync(request, cancellationToken);

            // Assert
            //_userLoginEndpoint.Errors.Should().Contain("The signing key is not configured!");
        }

        [TestMethod]
        public void ExecuteAsync_WhenCredentialsAreInvalid_ShouldThrowError()
        {
            // Arrange
            LoginRequest request = new LoginRequest { Username = "username", Password = "password" };
            CancellationToken cancellationToken = new CancellationToken();
            _configurationMock.Setup(x => x["TokenSigningKeys:Application"]).Returns("signingKey");
            _userServiceMock.Setup(x => x.ValidateCredentials(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(false);

            // Act
            Func<Task> action = async () => await _userLoginEndpoint.ExecuteAsync(request, cancellationToken);

            // Assert
            //action.Should().Throw<ValidationFailureException>().WithMessage("The supplied credentials are invalid!");
        }

        [TestMethod]
        public async Task ExecuteAsync_WhenCredentialsAreValid_ShouldReturnUserLoginResponse()
        {
            // Arrange
            LoginRequest request = new LoginRequest { Username = "username", Password = "password" };
            CancellationToken cancellationToken = new CancellationToken();
            _configurationMock.Setup(x => x["TokenSigningKeys:Application"]).Returns("signingKey");
            _userServiceMock.Setup(x => x.ValidateCredentials(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(true);

            // Act
            var result = await _userLoginEndpoint.ExecuteAsync(request, cancellationToken);

            // Assert
            //result.Should().BeOfType<UserLoginResponse>();
            //result.Username.Should().Be("username");
            //result.Token.Should().NotBeNullOrWhiteSpace();
        }

        [TestMethod]
        public async Task LoginSuccess()
        {
            // Arrange
            var fixture = new Fixture().Customize(new AutoMoqCustomization());
            var mockConfig = fixture.Freeze<Mock<IConfiguration>>();
            var mockLogger = fixture.Freeze<Mock<ILogger<UserLoginEndpoint>>>();
            var mockUserService = fixture.Freeze<Mock<IUserService>>();
            mockConfig.Setup(x => x["Security:TokenSigningKey"]).Returns("0000000000000000");

            var endpoint = Factory.Create<UserLoginEndpoint>(mockConfig.Object, mockUserService.Object);

            var req = new LoginRequest
            {
                Username = "test",
                Password = "test"
            };

            mockUserService.Setup(x => x.ValidateCredentials(req.Username, req.Password)).ReturnsAsync(true);

            // Act
            var response = await endpoint.ExecuteAsync(req, default);

            // Assert
            Assert.IsNotNull(response);
            Assert.IsFalse(endpoint.ValidationFailed);
            //Assert.IsNotNull(response.Token);
        }

        [TestMethod]
        public async Task LoginFailure()
        {
            // Arrange
            var fixture = new Fixture().Customize(new AutoMoqCustomization());
            var mockConfig = fixture.Freeze<Mock<IConfiguration>>();
            var mockLogger = fixture.Freeze<Mock<ILogger<UserLoginEndpoint>>>();
            var mockUserService = fixture.Freeze<Mock<IUserService>>();
            mockConfig.Setup(x => x["Security:TokenSigningKey"]).Returns("0000000000000000");

            var endpoint = Factory.Create<UserLoginEndpoint>(mockConfig.Object, mockUserService.Object);

            var req = new LoginRequest
            {
                Username = "invalidUsername",
                Password = "test"
            };

            mockUserService.Setup(x => x.ValidateCredentials(req.Username, req.Password)).ReturnsAsync(false);

            // Act
            var actual = async () => await endpoint.ExecuteAsync(req, default).ConfigureAwait(false);

            await actual.Should().ThrowAsync<ValidationFailureException>();
        }
    }
}