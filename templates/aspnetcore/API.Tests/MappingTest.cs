using API.ViewModels;
using FluentAssertions;
using Xunit;

namespace API.Tests
{
    public class MappingTest
    {
        [Fact]
        public void ToBusiness()
        {
            ExampleViewModel sut = new ExampleViewModel
            {
                Age = 18,
                Email = "biio@boop.com",
                Id = 1,
                Name = "hellop"
            };

            var business = sut.ToBusiness();

            business.Age.Should().Be(sut.Age);
            business.Email.Should().Be(sut.Email);
            business.Id.Should().Be(sut.Id);
            business.Name.Should().Be(sut.Name);
        }
    }
}
