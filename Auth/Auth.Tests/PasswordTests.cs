using Auth.Domain.Data.ValueObjects;
using Auth.Infrastructure.Security.Passwords;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Auth.Tests
{
    public class PasswordTests
    {
        [Fact]
        public void Test_GenerateValidPasswords()
        {
            for(int i=0;i<100;i++)
            {
                UserPassword.Randomize();
            }
        }
    }
}
