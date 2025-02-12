using MonoTask.Application.Services;
using MonoTask.Core.Entities.Entities;
using MonoTask.Infrastructure.Data.Entities;

namespace MonoTask.Tests.Services
{

    using AutoMapper;
    using global::MonoTask.Tests.Helpers.Builders;
    using Moq;
    using NUnit.Framework;
    using System.Linq;
    using System.Threading.Tasks;

    namespace MonoTask.Tests.Services
    {
        public class UserServicesTests
        {
            private DummyContext _context;
            private Mock<IMapper> _mockMapper;
            private UserServices _userServices;

            [SetUp]
            public void Setup()
            {

                _context = new DummyContext();
                _mockMapper = new Mock<IMapper>();
                _userServices = new UserServices(_context, _mockMapper.Object);

                _mockMapper.Setup(m => m.Map<User>(It.IsAny<UserEntity>()))
                   .Returns((UserEntity userEntity) =>
                       new User
                       {
                           Id = userEntity.Id,
                           AccessToken = userEntity.Token.AccessToken,
                           Name = userEntity.Name
                       });

            }

            [Test]
            public async Task InsertUser_ShouldInsertUserAndReturnMappedUser()
            {
                // Arrange
                var name = "Test User";
                var email = "Test User";
                var password = "Test User";

                // Act
                var user = await _userServices.InsertUser(name, email, password);

                // Assert
                var addedUser = _context.Users.FirstOrDefault(t => t.Id == user.Id);
                Assert.That(addedUser, Is.Not.Null);
                Assert.That(addedUser.Name, Is.EqualTo(name));
                Assert.That(addedUser.Email, Is.EqualTo(email));
                Assert.That(addedUser.Password, Is.EqualTo(password));

                var addedToken = _context.UserTokens.FirstOrDefault(t => t.Id == addedUser.TokenId);
                Assert.That(addedToken, Is.Not.Null);
                Assert.That(addedToken.AccessToken, Is.Not.Empty);
                Assert.That(addedToken.RefreshToken, Is.Not.Empty);
                Assert.That(addedToken.AccessTokenExpiry, Is.GreaterThan(DateTime.UtcNow));
            }

            [Test]
            public async Task RefreshToken_ShouldRefreshAccessTokenAndReturnMappedUser()
            {
                // Arrange
                int userId = 1;
                var refreshToken = "validRefreshToken";
                var oldAccessToken = "oldAccessToken";
                var tokenExpiry = DateTime.UtcNow.AddMinutes(-10);

                var token = new TokenBuilder(userId)
                    .WithRefreshToken(refreshToken)
                    .WithAccessToken(oldAccessToken)
                    .WithAccessTokenExpiry(tokenExpiry)
                    .Build();

                var user = new UserBuilder()
                    .WithId(userId)
                    .WithToken(token)
                    .Build();

                _context.UserTokens.Add(token);
                _context.Users.Add(user);

                // Act
                var result = await _userServices.RefreshToken(refreshToken);

                // Assert
                var newToken = _context.UserTokens.FirstOrDefault(t => t.UserId == result.Id);
                Assert.That(newToken, Is.Not.Null);
                Assert.That(newToken.AccessToken, Is.Not.Empty);
                Assert.That(newToken.AccessToken, Is.Not.EqualTo(oldAccessToken));
                Assert.That(newToken.RefreshToken, Is.Not.Empty);
                Assert.That(newToken.RefreshToken, Is.EqualTo(refreshToken));
                Assert.That(newToken.AccessTokenExpiry, Is.GreaterThan(DateTime.UtcNow));
            }

            [Test]
            public async Task RefreshToken_ShouldThrowException_WhenRefreshTokenIsInvalid()
            {
                // Arrange
                var invalidRefreshToken = "invalidRefreshToken";

                // Act & Assert
                var exception = Assert.ThrowsAsync<ArgumentException>(async () =>
                    await _userServices.RefreshToken(invalidRefreshToken));

                Assert.That(exception.Message, Is.EqualTo("Can't authorize"));
            }
        }
    }
}
