using Moq;
using NJsonSchema;
using NJsonSchema.Generation;
using RA;
using System.Net.Http;
using Wiz.Template.Domain.Models.Services;
using Xunit;

namespace Wiz.Template.Contract.Tests.Consumer
{
    public class ViaCEPTest
    {
        private readonly Mock<HttpMessageHandler> _httpMessageHandlerMock;

        public ViaCEPTest()
        {
            _httpMessageHandlerMock = new Mock<HttpMessageHandler>();
        }

        [Theory]
        [InlineData("17052520")]
        [InlineData("17052-520")]
        public void GetByCEPAsync_Functional_Test(string cep)
        {
            var httpClientMock = "https://viacep.com.br/ws/";

            new RestAssured()
             .Given()
                 .Name("VIA Cep Teste")
                 .Header("Content-Type", "text/html; charset=utf-8")
                 .Header("Accept-Encoding", "gzip,deflate")
             .When()
                 .Get($"{httpClientMock}{cep}/json/")
             .Then()
                 //.Debug()
                 .TestStatus("test status", x => x == 200)
                 .TestBody("test cep", x => x.cep != null)
                 .TestElaspedTime("test response time", x => x < 1000)
                 .WriteAssertions()
                 .AssertAll();
        }

        [Theory]
        [InlineData("17052520")]
        [InlineData("17052-520")]
        public void GetByCEPAsync_ResponseSchema_Test(string cep)
        {
            var httpClientMock = "https://viacep.com.br/ws/";

            string schema = JsonSchema.FromType<ViaCEP>(new JsonSchemaGeneratorSettings(){
                AlwaysAllowAdditionalObjectProperties = true
            }).ToJson();

            new RestAssured()
             .Given()
                 .Name("VIA Cep Teste")
                 .Header("Content-Type", "text/html; charset=utf-8")
                 .Header("Accept-Encoding", "gzip,deflate")
             .When()
                 .Get($"{httpClientMock}{cep}/json/")
             .Then()
                 //.Debug()
                 .Schema(schema)
                 .WriteAssertions()
                 .AssertSchema();
        }
    }

}
