using System.Data;
using AutoFixture;
using AutoFixture.AutoMoq;
using DataLayer.Data;
using EntityFrameworkCore.AutoFixture.InMemory;
using FluentAssertions;
using FluentAssertions.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Services.EfCore.Tests.UnitTests
{
    [TestClass]
    public class UserServiceTests : VerifyBase
    {
        [TestMethod]
        public async Task CreateUserTest()
        {
            var fixture = new Fixture().Customize(new InMemoryCustomization()).Customize(new AutoMoqCustomization());
            fixture.Freeze<sewright22_foodjournalContext>();
            var mockHasher = fixture.Freeze<Mock<IPasswordHasher<User>>>();

            mockHasher.Setup(x => x.HashPassword(It.IsAny<User>(), "password")).Returns("encryptedPassword");

            var userService = fixture.Create<UserService>();

            var actual = await userService.AddUser("username", "password").ConfigureAwait(false);

            await this.Verify(actual).ConfigureAwait(false);
        }

        [TestMethod]
        public async Task UserAlreadyExists()
        {
            var fixture = new Fixture().Customize(new InMemoryCustomization()).Customize(new AutoMoqCustomization());
            var dbContext = fixture.Freeze<sewright22_foodjournalContext>();

            dbContext.Add(new User
            {
                Email = "username",
            });

            await dbContext.SaveChangesAsync().ConfigureAwait(false);

            var userService = fixture.Create<UserService>();

            var addUserAction = async () => { await userService.AddUser("username", "password").ConfigureAwait(false); };
            await addUserAction.Should().ThrowAsync<ArgumentException>().WithMessage("User already exists!").ConfigureAwait(false);
        }

        [TestMethod]
        public async Task NullUsernameIsGivenToCreateUser()
        {
            var fixture = new Fixture().Customize(new InMemoryCustomization()).Customize(new AutoMoqCustomization());
            fixture.Freeze<sewright22_foodjournalContext>();

            var userService = fixture.Create<UserService>();

            var addUserAction = async () => { await userService.AddUser(null!, "password").ConfigureAwait(false); };
            await addUserAction.Should().ThrowAsync<ArgumentNullException>().WithParameterName("userName").ConfigureAwait(false);
        }

        [TestMethod]
        public async Task NullPasswordIsGivenToCreateUser()
        {
            var fixture = new Fixture().Customize(new InMemoryCustomization()).Customize(new AutoMoqCustomization());
            fixture.Freeze<sewright22_foodjournalContext>();

            var userService = fixture.Create<UserService>();

            var addUserAction = async () => { await userService.AddUser("username", null!).ConfigureAwait(false); };
            await addUserAction.Should().ThrowAsync<ArgumentNullException>().WithParameterName("password").ConfigureAwait(false);
        }

        [TestMethod]
        public async Task ValidCredentialsTest()
        {
            // Arrange
            var fixture = new Fixture()
                .Customize(new InMemoryCustomization())
                .Customize(new AutoMoqCustomization());

            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            var dbContext = fixture.Freeze<sewright22_foodjournalContext>();
            var mockHasher = fixture.Freeze<Mock<IPasswordHasher<User>>>();
            var email = "test@test.com";
            var passwordHash = "ASDFJKIASNDFIKASLEDF";
            var password = "password";

            var user = fixture.Build<User>()
                .With(x=>x.Email, email)
                .With(x => x.Userpassword, fixture.Build<Userpassword>()
                .With(x => x.Password, fixture.Build<Password>()
                .With(x => x.Text, passwordHash)
                .Create())
                .Create()).Create();

            dbContext.Add(user);
            dbContext.SaveChanges();

            mockHasher.Setup(x => x.VerifyHashedPassword(It.IsAny<User>(), passwordHash, "password")).Returns(PasswordVerificationResult.Success);

            // Act
            var userService = fixture.Create<UserService>();

            var actualResult = await userService.ValidateCredentials(email, password).ConfigureAwait(false);

            // Assert
            actualResult.Should().BeTrue();
            mockHasher.Verify(x => x.HashPassword(It.IsAny<User>(), password), Times.Never);
        }

        [TestMethod]
        public async Task InvalidCredentialsTest()
        {
            var fixture = new Fixture()
                .Customize(new InMemoryCustomization())
                .Customize(new AutoMoqCustomization());

            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            var dbContext = fixture.Freeze<sewright22_foodjournalContext>();
            var mockHasher = fixture.Freeze<Mock<IPasswordHasher<User>>>();
            var email = "test@test.com";
            var passwordHash = "ASDFJKIASNDFIKASLEDF";
            var password = "password";

            var user = fixture.Build<User>()
                .With(x => x.Email, email)
                .With(x => x.Userpassword, fixture.Build<Userpassword>()
                .With(x => x.Password, fixture.Build<Password>()
                .With(x => x.Text, passwordHash)
                .Create())
                .Create()).Create();

            dbContext.Add(user);
            dbContext.SaveChanges();

            mockHasher.Setup(x => x.VerifyHashedPassword(It.IsAny<User>(), passwordHash, "password")).Returns(PasswordVerificationResult.Failed);

            var userService = fixture.Create<UserService>();

            var actual = await userService.ValidateCredentials(email, password).ConfigureAwait(false);

            actual.Should().BeFalse();
            mockHasher.Verify(x => x.HashPassword(It.IsAny<User>(), password), Times.Never);
        }

        [TestMethod]
        public async Task RehashNeededTest()
        {
            var fixture = new Fixture()
                .Customize(new InMemoryCustomization())
                .Customize(new AutoMoqCustomization());

            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            var dbContext = fixture.Freeze<sewright22_foodjournalContext>();
            var mockHasher = fixture.Freeze<Mock<IPasswordHasher<User>>>();
            var email = "test@test.com";
            var passwordHash = "ASDFJKIASNDFIKASLEDF";
            var password = "password";

            var user = fixture.Build<User>()
                .With(x => x.Email, email)
                .With(x => x.Userpassword, fixture.Build<Userpassword>()
                .With(x => x.Password, fixture.Build<Password>()
                .With(x => x.Text, passwordHash)
                .Create())
                .Create()).Create();

            dbContext.Add(user);
            dbContext.SaveChanges();

            mockHasher.Setup(x => x.VerifyHashedPassword(It.IsAny<User>(), passwordHash, "password")).Returns(PasswordVerificationResult.SuccessRehashNeeded);

            var userService = fixture.Create<UserService>();

            var actual = await userService.ValidateCredentials(email, password).ConfigureAwait(false);

            actual.Should().BeTrue();

            mockHasher.Verify(x => x.HashPassword(It.IsAny<User>(), password), Times.Once);
        }

        [TestMethod]
        public async Task ErrorThrownWhenMoreThanOneUserIsFoundInDatabase()
        {
            // Arrange
            var fixture = new Fixture()
                .Customize(new InMemoryCustomization())
                .Customize(new AutoMoqCustomization());

            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            var dbContext = fixture.Freeze<sewright22_foodjournalContext>();
            var mockHasher = fixture.Freeze<Mock<IPasswordHasher<User>>>();
            var email = "test@test.com";
            var passwordHash = "ASDFJKIASNDFIKASLEDF";
            var password = "password";

            var user = fixture.Build<User>()
                .With(x => x.Email, email)
                .With(x => x.Userpassword, fixture.Build<Userpassword>()
                .With(x => x.Password, fixture.Build<Password>()
                .With(x => x.Text, passwordHash)
                .Create())
                .Create()).Create();

            var user2 = fixture.Build<User>()
                .With(x => x.Email, email)
                .With(x => x.Userpassword, fixture.Build<Userpassword>()
                .With(x => x.Password, fixture.Build<Password>()
                .With(x => x.Text, passwordHash)
                .Create())
                .Create()).Create();

            dbContext.Add(user);
            dbContext.Add(user2);
            dbContext.SaveChanges();

            mockHasher.Setup(x => x.VerifyHashedPassword(It.IsAny<User>(), passwordHash, "password")).Returns(PasswordVerificationResult.Success);

            // Act
            var userService = fixture.Create<UserService>();

            var func = async () => await userService.ValidateCredentials(email, password).ConfigureAwait(false);

            await func.Should().ThrowAsync<DuplicateNameException>().ConfigureAwait(false);
        }
    }
}