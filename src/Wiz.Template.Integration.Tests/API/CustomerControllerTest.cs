using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Wiz.Template.API;
using Wiz.Template.Integration.Tests.Mocks;
using Xunit;

namespace Wiz.Template.Integration.Tests.API
{
    public class CustomerControllerTest : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly HttpClient _httpClient;

        public CustomerControllerTest(WebApplicationFactory<Startup> factory)
        {
            _httpClient = factory.CreateClient();
        }

        [Fact]
        public async Task GetAll_HttpStatusCodeUnauthorizedTestAsync()
        {
            var response = await _httpClient.GetAsync("/api/v1/customers");

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task GetById_HttpStatusCodeUnauthorizedTestAsync()
        {
            var customerId = CustomerMock.CustomerIdViewModelFaker.Generate();

            var response = await _httpClient.GetAsync($"/api/v1/customers/{customerId}");

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task GetByName_HttpStatusCodeUnauthorizedTestAsync()
        {
            var customerName = CustomerMock.CustomerNameViewModelFaker.Generate();

            var response = await _httpClient.GetAsync($"/api/v1/customers/name/{customerName}");

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task Post_HttpStatusCodeUnauthorizedTestAsync()
        {
            var customer = CustomerMock.CustomerViewModelFaker.Generate();

            var response = await _httpClient.PostAsync("/api/v1/customers", new StringContent(JsonConvert.SerializeObject(customer)));

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task Put_HttpStatusCodeUnauthorizedTestAsync()
        {
            var id = 1;
            var customer = CustomerMock.CustomerViewModelFaker.Generate();

            var response = await _httpClient.PutAsync($"/api/v1/customers/{id}", new StringContent(JsonConvert.SerializeObject(customer)));

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task Delete_HttpStatusCodeUnauthorizedTestAsync()
        {
            var id = 1;
            var response = await _httpClient.DeleteAsync($"/api/v1/customers/{id}");

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }
    }
}
