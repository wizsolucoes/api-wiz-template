using FluentAssertions;
using LanguageExt;
using MediatR;
using Moq;
using Wiz.Template.API.Services;
using Wiz.Template.API.ViewModels.CepViewModels;
using Wiz.Template.Domain.Interfaces.HttpServices;
using Wiz.Template.Domain.Models;
using Wiz.Template.Shared.Mocks;
using Xunit;

namespace Wiz.Template.Unit.Tests.API.Services
{
    public class GetInformacaoCepHandlerTest
    {
        private readonly Mock<IViaCepService> _service;
        private readonly IRequestHandler<RequestInformacaoCepViewModel, Option<ViaCep>> _handler;

        public GetInformacaoCepHandlerTest()
        {
            _service = new Mock<IViaCepService>();
            _handler = new GetInformacaoCepHandler(_service.Object);
        }

        [Fact]
        public async void Deve_Retornar_Objeto_ViaCep_Com_Informacoes()
        {
            // Arrange
            var request = ExampleMock.RequestInformacaoCepViewModelFaker;

            _service.Setup(
                x => x.BuscarInformacoesCep(It.IsAny<string>())
            ).ReturnsAsync(ExampleMock.ViaCepFaker.Generate());

            // Act
            var option = await _handler.Handle(request, default);
            var viaCep = option.Head();

            // Assert
            option.IsNone.Should().BeFalse();
            viaCep.Logradouro.Should().Be("SQN 210 Bloco E");
        }

        [Fact]
        public async void Deve_Retornar_Objeto_ViaCep_None_Caso_Erro()
        {
            // Arrange
            var request = ExampleMock.RequestInformacaoCepViewModelFaker;

            _service.Setup(
                x => x.BuscarInformacoesCep(It.IsAny<string>())
            ).ReturnsAsync(Option<ViaCep>.None);

            // Act
            var option = await _handler.Handle(request, default);

            // Assert
            option.IsNone.Should().BeTrue();
        }
    }
}
