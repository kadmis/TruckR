using Auth.Domain.Data.Entities;
using Auth.Domain.Data.ValueObjects;
using Auth.Domain.Persistence;
using Auth.Domain.Security.Passwords;
using Auth.Domain.Services.Authentication;
using Auth.Domain.Services.Registration;
using Auth.Domain.Specifications.EmailSpecifications;
using Auth.Domain.Specifications.PasswordSpecifications;
using Auth.Domain.Specifications.UsernameSpecifications;
using Moq;
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

            return new UserRegistrationService(_uowMock.Object, emailSpec, usernameSpec);
        }

        private IUserAuthenticationService SetupAuthenticationService()
        {
            _uowMock = new();
            _hasherMock = new();
            var passwordSpec = new PasswordMatches(_hasherMock.Object);

            return new UserAuthenticationService(_uowMock.Object, passwordSpec);
        }

        [Fact]
        public async Task RegisterDriver_ShouldSucceed()
        {
            //Assign
            var sut = SetupRegistrationService();
            var validUsername = "validusername";
            var validEmail = "valid@email.com";
            var validPassword = "ValidPassword123^";
            var validPhoneNumber = "+48665467131";
            var firstname = "Bob";
            var lastname = "Dylan";

            _uowMock
                .Setup(x => x.UserRepository.EmailExists(It.IsAny<Email>(), CancellationToken.None))
                .ReturnsAsync(false);
            _uowMock
                .Setup(x => x.UserRepository.UsernameExists(It.IsAny<Username>(), CancellationToken.None))
                .ReturnsAsync(false);

            //Act
            var registeredUser = await sut.RegisterDriver(validUsername, firstname, lastname, validPassword, validEmail, validPhoneNumber);

            //Assert
            Assert.NotNull(registeredUser);
        }

        [Fact]
        public async Task AuthenticateUser_ShouldSucceed()
        {
            //Assign
            var sut = SetupAuthenticationService();
            var validUsername = "validusername";
            var validPassword = "ValidPassword123^";
            var validEmail = "valid@email.com";
            var validPhoneNumber = "+48665467131";
            var firstname = "Bob";
            var lastname = "Dylan";
            var user = User.Create(validUsername, firstname, lastname, validPassword, validEmail, validPhoneNumber, UserRole.Driver);
            user.Activate(user.ActivationId.Value);

            _uowMock
                .Setup(x => x.UserRepository.FindByUsername(It.IsAny<Username>(), CancellationToken.None))
                .ReturnsAsync(() => user);
            _hasherMock
                .Setup(x => x.VerifyHashedPassword(It.IsAny<Password>(), It.IsAny<Password>()))
                .Returns(true);

            //Act
            var result = await sut.Authenticate(validUsername, validPassword);

            //Assert
            Assert.NotNull(result);
        }
    }
}
