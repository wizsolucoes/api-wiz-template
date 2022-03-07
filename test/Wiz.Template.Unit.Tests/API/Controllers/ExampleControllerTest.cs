using FluentAssertions;
using LanguageExt;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Wiz.Template.API.Controllers;
using Wiz.Template.API.ViewModels.Example;
using Wiz.Template.Domain.Entities;
using Wiz.Template.Shared.Mocks;
using Xunit;

namespace Wiz.Template.Unit.Tests.API.Controllers
{
    public class ExampleControllerTest
    {
        private readonly Mock<ILogger<ExampleController>> _logger;
        private readonly Mock<IMediator> _mediator;
        private readonly ExampleController _controller;

        public ExampleControllerTest()
        {
            _logger = new Mock<ILogger<ExampleController>>();
            _mediator = new Mock<IMediator>();
            _controller = new ExampleController(
                _logger.Object,
                _mediator.Object
            );
        }

        [Fact]
        public async void Deve_Retornar_StatusCode_Ok_Caso_Encontre_Example()
        {
            // Arrange
            var request = ExampleMock.RequestExampleViewModelFaker;

            _mediator.Setup(
                x => x.Send<Option<Example>>(
                    It.IsAny<RequestExampleViewModel>(),
                    default
                )
            ).ReturnsAsync(ExampleMock.ExampleFaker.Generate());

            // Act
            var result = await _controller.GetMediatR(request);

            // Assert
            result.Should().BeOfType<ActionResult<ResponseExampleViewModel>>();
            result.Result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async void Deve_Retornar_StatusCode_NotFound_Caso_Nao_Encontre_Example()
        {
            // Arrange
            var request = ExampleMock.RequestExampleViewModelFaker;

            _mediator.Setup(
                x => x.Send<Option<Example>>(
                    It.IsAny<RequestExampleViewModel>(),
                    default
                )
            ).ReturnsAsync((Example)null);

            // Act
            var result = await _controller.GetMediatR(request);

            // Assert
            result.Should().BeOfType<ActionResult<ResponseExampleViewModel>>();
            result.Result.Should().BeOfType<NotFoundResult>();
        }
    }
}
