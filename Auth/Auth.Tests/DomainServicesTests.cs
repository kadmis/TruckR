using Auth.Domain.Data.ValueObjects;
using Auth.Domain.Persistence;
using Auth.Domain.Services.Registration;
using Auth.Domain.Specifications.EmailSpecifications;
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

        private IUserRegistrationService SetupRegistrationService()
        {
            _uowMock = new();
            var emailSpec = new EmailExists(_uowMock.Object);
            var usernameSpec = new UsernameExists(_uowMock.Object);

            return new UserRegistrationService(_uowMock.Object, emailSpec, usernameSpec);
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
    }
}
