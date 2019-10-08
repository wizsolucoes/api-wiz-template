using Moq;
using Moq.Protected;
using System;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Wiz.Template.Domain.Models.Services;
using Wiz.Template.Infra.Services;
using Wiz.Template.Integration.Tests.Mocks;
using Xunit;

namespace Wiz.Template.Integration.Tests.Services
{
    public class ViaCEPServiceTest
    {
        private readonly Mock<HttpMessageHandler> _httpMessageHandlerMock;

        public ViaCEPServiceTest()
        {
            _httpMessageHandlerMock = new Mock<HttpMessageHandler>();
        }

        [Fact]
        public async Task GetByCEPAsync_ReturnViaCEPModelTestAsync()
        {
            var httpClientMock = "https://viacep.com.br/ws/";
            var cep = "17052520";

            _httpMessageHandlerMock.Protected().Setup<Task<HttpResponseMessage>>("SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(JsonSerializer.Serialize(ViaCEPMock.ViaCEPModelFaker.Generate()))
                });

            var httpClient = new HttpClient(_httpMessageHandlerMock.Object)
            {
                BaseAddress = new Uri(httpClientMock)
            };

            var viaCEPservice = new ViaCEPService(httpClient);
            var viaCEPMethod = await viaCEPservice.GetByCEPAsync(cep);

            var serviceResult = Assert.IsType<ViaCEP>(viaCEPMethod);

            Assert.NotNull(serviceResult);
            Assert.NotNull(serviceResult.CEP);
        }
    }
}
