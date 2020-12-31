using API.Validators;
using API.ViewModels;
using FluentAssertions;
using Xunit;

namespace API.Tests
{
    public class ValidationTest
    {
        private readonly ExampleValidator sut;

        public ValidationTest()
        {
            this.sut = new ExampleValidator();
        }

        [Fact]
        public void IsValid()
        {
            ExampleViewModel model = new ExampleViewModel 
            {
                Age = 20,
                Email = "test@email.com",
                Id = 1,
                Name = "hello world"
            };

            var result = this.sut.Validate(model);

            result.IsValid.Should().BeTrue();
        }
    }
}
