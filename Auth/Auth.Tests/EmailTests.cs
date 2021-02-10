using Auth.Domain.Data.ValueObjects;
using System;
using Xunit;

namespace Auth.Tests
{
    public class EmailTests
    {
        [Fact]
        public void Test_ShouldCreateValidEmail()
        {
            var emailString = "test+2@gmail.com";
            var email = new UserEmail(emailString);
            Assert.Equal(emailString, email.Email);
        }
    }
}
