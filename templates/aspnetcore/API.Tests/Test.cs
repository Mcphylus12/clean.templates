using System;
using Xunit;

namespace API.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void TestTest()
        {
            Assert.True(true);
        }

        [Fact]
        public void FailingTest()
        {
            Assert.True(false);
        }
    }
}
