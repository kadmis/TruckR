using Auth.Domain.Data.ValueObjects;
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
                Password.Randomize();
            }
        }
    }
}
