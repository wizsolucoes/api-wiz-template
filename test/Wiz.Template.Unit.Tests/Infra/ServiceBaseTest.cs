using System.Net;
using System.Net.Http;
using System.Text;
using FluentAssertions;
using Wiz.Template.Domain.Models;
using Wiz.Template.Infra.HttpServices;
using Xunit;

namespace Wiz.Template.Unit.Tests.Infra
{
    public class ServiceBaseTest
    {
        private readonly ServiceBase _serviceBase;

        public ServiceBaseTest()
        {
            _serviceBase = new ServiceBase();
        }

        [Fact]
        public async void Deve_Retornar_Objeto_Serializado()
        {
            // Arrange
            var obj = new { Lorem = "Ipsum" };

            // Act
            var stringContent = _serviceBase.AssembleJsonContent(obj);
            var jsonString = await stringContent.ReadAsStringAsync();

            // Assert
            jsonString.Should().Be("{\"Lorem\":\"Ipsum\"}");
        }

        [Fact]
        public async void Deve_Retornar_Objeto_Deserializado()
        {
            // Arrange
            var responseMessage = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(
                    "{\"erro\":true}",
                    Encoding.UTF8,
                    "application/json"
                )
            };

            // Act
            var obj = await _serviceBase.HandleResponse<ViaCep>(responseMessage);
            var viaCep = obj.Head();

            // Assert
            viaCep.Should().NotBeNull();
            viaCep.Erro.Should().BeTrue();
        }

        [Fact]
        public async void Deve_Retornar_Objeto_None()
        {
            // Arrange
            var responseMessage = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.BadRequest
            };

            // Act
            var obj = await _serviceBase.HandleResponse<ViaCep>(responseMessage);

            // Assert
            obj.IsNone.Should().BeTrue();
        }
    }
}
