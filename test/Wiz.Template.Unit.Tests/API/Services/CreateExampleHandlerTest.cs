using FluentAssertions;
using LanguageExt;
using MediatR;
using Moq;
using Wiz.Template.API.Services;
using Wiz.Template.API.ViewModels.ExampleViewModels;
using Wiz.Template.Domain.Entities;
using Wiz.Template.Domain.Interfaces.Repositories;
using Wiz.Template.Domain.Interfaces.UoW;
using Wiz.Template.Shared.Mocks;
using Xunit;

namespace Wiz.Template.Unit.Tests.API.Services
{
    public class CreateExampleHandlerTest
    {
        private readonly Mock<IExampleRepository> _repository;
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly IRequestHandler<RequestCreateExampleViewModel, Option<Example>> _handler;

        public CreateExampleHandlerTest()
        {
            _repository = new Mock<IExampleRepository>();
            _unitOfWork = new Mock<IUnitOfWork>();

            _handler = new CreateExampleHandler(
                _repository.Object,
                _unitOfWork.Object
            );
        }

        [Theory]
        [InlineData(-101)]
        [InlineData(101)]
        public async void Deve_Retornar_Option_None_Caso_Examplo_Invalido(
            int temperatureC
        )
        {
            // Arrange
            var request = ExampleMock.RequestCreateExampleViewModelFaker
                .RuleFor(x => x.TemperatureC, temperatureC);

            // Act
            var example = await _handler.Handle(request, default);

            // Assert
            example.IsNone.Should().BeTrue();

            _repository.Verify(x => x.Add(It.IsAny<Example>()), Times.Never);
            _unitOfWork.Verify(x => x.Commit(), Times.Never);
        }

        [Fact]
        public async void Deve_Retornar_Example_Criado()
        {
            // Arrange
            var request = ExampleMock.RequestCreateExampleViewModelFaker;

            // Act
            var example = await _handler.Handle(request, default);

            // Assert
            example.IsNone.Should().BeFalse();

            _repository.Verify(x => x.Add(It.IsAny<Example>()), Times.Once);
            _unitOfWork.Verify(x => x.Commit(), Times.Once);
        }
    }
}
