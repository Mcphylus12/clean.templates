using API.Controllers;
using API.ViewModels;
using Business;
using Business.Tests.Helpers;
using DB.Tests.DBHelpers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Xunit;

namespace API.Tests
{
    public class ControllerTest
    {
        private readonly ExampleController sut;
        private readonly MockMediator mediatorMock;
        private readonly IRepository<ExampleBusinessModel> repository;

        public ControllerTest()
        {
            this.mediatorMock = new MockMediator();
            this.repository = InMemoryDb.GetRepository<ExampleBusinessModel>();
            this.sut = new ExampleController(repository, mediatorMock);
        }

        [Fact]
        public async Task BuinessFunctionCall()
        {
            mediatorMock.Returns<ExampleParam, ExampleResult>(new ExampleResult(false));

            var result = await sut.ExampleBusinessFunction(1, new ExampleBusinessFunctionParameters
            {
                AgeToAdd = 5
            });

            result.Should().BeOfType<NotFoundResult>();
        }
    }
}
