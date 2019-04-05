using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Wiz.Template.API;
using Wiz.Template.Tests.Mocks.ViewModels;
using Xunit;

namespace Wiz.Template.Tests.Integration.API
{
    public class CustomerIntegrationAPITest : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly HttpClient _httpClient;

        public CustomerIntegrationAPITest(WebApplicationFactory<Startup> factory)
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
            var id = 1;
            var customerId = CustomerViewModelMock.GetCustomerId(id);

            var response = await _httpClient.GetAsync($"/api/v1/customers/{customerId}");

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task GetByName_HttpStatusCodeUnauthorizedTestAsync()
        {
            var name = "Diuor PleaBolosmakh";
            var customerName = CustomerViewModelMock.GetCustomerName(name);

            var response = await _httpClient.GetAsync($"/api/v1/customers/name/{customerName}");

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task Post_HttpStatusCodeUnauthorizedTestAsync()
        {
            var customer = CustomerViewModelMock.GetCustomer();

            var response = await _httpClient.PostAsync("/api/v1/customers", new StringContent(JsonConvert.SerializeObject(customer)));

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task Put_HttpStatusCodeUnauthorizedTestAsync()
        {
            var id = 1;
            var customer = CustomerViewModelMock.GetCustomer();

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
