using Bogus;
using FluentAssertions;
using LanguageExt;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Wiz.Template.API.Controllers;
using Wiz.Template.API.ViewModels.ExampleViewModels;
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
            ).ReturnsAsync(Option<Example>.None);

            // Act
            var result = await _controller.GetMediatR(request);

            // Assert
            result.Should().BeOfType<ActionResult<ResponseExampleViewModel>>();
            result.Result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async void Deve_Retornar_StatusCode_Created_Ao_Criar_Novo_Example()
        {
            // Arrange
            var request = ExampleMock.RequestCreateExampleViewModelFaker;

            _mediator.Setup(
                x => x.Send<Option<Example>>(
                    It.IsAny<RequestCreateExampleViewModel>(),
                    default
                )
            ).ReturnsAsync(ExampleMock.ExampleFaker.Generate());

            // Act
            var result = await _controller.CreateExample(request);

            // Assert
            result.Should().BeOfType<ActionResult<ResponseExampleViewModel>>();
            result.Result.Should().BeOfType<CreatedResult>();
        }

        [Fact]
        public async void Deve_Retornar_StatusCode_BadRequest_Ao_Criar_Novo_Example()
        {
            // Arrange
            var request = ExampleMock.RequestCreateExampleViewModelFaker;

            _mediator.Setup(
                x => x.Send<Option<Example>>(
                    It.IsAny<RequestCreateExampleViewModel>(),
                    default
                )
            ).ReturnsAsync(Option<Example>.None);

            // Act
            var result = await _controller.CreateExample(request);

            // Assert
            result.Should().BeOfType<ActionResult<ResponseExampleViewModel>>();
            result.Result.Should().BeOfType<BadRequestResult>();
        }

        [Fact]
        public void Deve_Retornar_StatusCode_NoContent_Ao_Passar_Parametro_Rota()
        {
            // Arrange
            var param = new Faker().Random.Word();

            // Act
            var result = _controller.GetRouteParam(param);

            // Assert
            result.Should().BeOfType<NoContentResult>();
        }
    }
}
