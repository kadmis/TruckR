using Auth.Domain.Creation;
using Auth.Domain.Data.Entities;
using Auth.Domain.Data.ValueObjects;
using Auth.Domain.Persistence;
using Auth.Domain.Security.Passwords;
using Auth.Domain.Services.Authentication;
using Auth.Domain.Services.Registration;
using Auth.Domain.Specifications.EmailSpecifications;
using Auth.Domain.Specifications.EmailSpecifications.Interfaces;
using Auth.Domain.Specifications.PasswordSpecifications;
using Auth.Domain.Specifications.UsernameSpecifications;
using Auth.Domain.Specifications.UsernameSpecifications.Interfaces;
using Auth.Infrastructure.Creation;
using Auth.Infrastructure.Security.Passwords;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Auth.Tests
{
    public class DomainServicesTests
    {
        private Mock<IUnitOfWork> _uowMock;
        private Mock<IPasswordHasher> _hasherMock;

        private IUserRegistrationService SetupRegistrationService()
        {
            _uowMock = new();
            var emailSpec = new EmailExists(_uowMock.Object);
            var usernameSpec = new UsernameExists(_uowMock.Object);

            return new UserRegistrationService(_uowMock.Object, new UserFactory(), emailSpec, usernameSpec);
        }

        private IUserAuthenticationService SetupAuthenticationService()
        {
            _uowMock = new();
            _hasherMock = new();
            var passwordSpec = new PasswordMatches(_hasherMock.Object);

            return new UserAuthenticationService(_uowMock.Object, passwordSpec);
        }

        [Fact]
        public async Task RegisterUser_ShouldSucceed()
        {
            var sut = SetupRegistrationService();
            var validUsername = "validusername";
            var validEmail = "valid@email.com";
            var validPassword = "ValidPassword123^";

            _uowMock
                .Setup(x => x.UserRepository.EmailExists(It.IsAny<Email>(), CancellationToken.None))
                .ReturnsAsync(false);
            _uowMock
                .Setup(x => x.UserRepository.UsernameExists(It.IsAny<Username>(), CancellationToken.None))
                .ReturnsAsync(false);

            var registeredUser = await sut.Register(validUsername, validPassword, validEmail);

            Assert.NotNull(registeredUser);
        }

        [Fact]
        public async Task AuthenticateUser_ShouldSucceed()
        {
            var sut = SetupAuthenticationService();
            var validUsername = "validusername";
            var validPassword = "ValidPassword123^";
            var validEmail = "valid@email.com";
            var user = new User(new Username(validUsername), new Password(validPassword), new Email(validEmail));
            user.Activate(user.ActivationId.Value);

            _uowMock
                .Setup(x => x.UserRepository.FindByUsername(It.IsAny<Username>(), CancellationToken.None))
                .ReturnsAsync(() => user);
            _hasherMock
                .Setup(x => x.VerifyHashedPassword(It.IsAny<Password>(), It.IsAny<Password>()))
                .Returns(true);

            var result = await sut.Authenticate(validUsername, validPassword);

            Assert.NotNull(result);
        }
    }
}
