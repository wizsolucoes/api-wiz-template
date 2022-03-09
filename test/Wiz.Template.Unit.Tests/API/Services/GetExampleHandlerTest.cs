using System;
using FluentAssertions;
using LanguageExt;
using MediatR;
using Wiz.Template.API.Services;
using Wiz.Template.API.ViewModels.ExampleViewModels;
using Wiz.Template.Domain.Entities;
using Wiz.Template.Shared.Mocks;
using Xunit;

namespace Wiz.Template.Unit.Tests.API.Services
{
    public class GetExampleHandlerTest
    {
        private readonly IRequestHandler<RequestExampleViewModel, Option<Example>> _handler;

        public GetExampleHandlerTest()
        {
            _handler = new GetExampleHandler();
        }

        [Theory]
        [InlineData(2022, 2, 2, false)]
        [InlineData(2022, 2, 3, true)]
        public async void Deve_Retornar_Example_Por_Data(
            int year, int month, int day, bool expectedNone
        )
        {
            // Arrange
            var request = ExampleMock.RequestExampleViewModelFaker
                .RuleFor(x => x.Date, new DateTime(year, month, day));

            // Act
            var result = await _handler.Handle(request, default);

            // Assert
            result.IsNone.Should().Be(expectedNone);
        }
    }
}
