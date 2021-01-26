using Microsoft.AspNetCore.Mvc.Testing;
using Moq;
using Newtonsoft.Json;
using System.Data;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Wiz.Template.API;
using Wiz.Template.Core.Tests.Mocks;
using Wiz.Template.Core.Tests.Mocks.Factory;
using Wiz.Template.Domain.Models.Dapper;
using Xunit;
using System.Linq;
using System.Collections.Generic;
using Dapper;
using System.Text;
using Moq.Dapper;

namespace Wiz.Template.Integration.Tests.API
{
    public class CustomerControllerTest : IClassFixture<MockWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _httpClient;

        public CustomerControllerTest(MockWebApplicationFactory<Startup> factory)
        {
            _httpClient = factory.CreateClient();
            SqlMapper.PurgeQueryCache();
        }

        [Fact]
        public async Task GetAll_HttpStatusCodeOKTestAsync()
        {
            MockAuthorizationFactory.AddAdminHeaders(_httpClient);

            var query = @"SELECT c.Id AS id, a.Id AS addressId, c.Name AS name, c.DateCreated AS dateCreated, a.CEP AS cep
                            FROM dbo.Customer c
                            INNER JOIN dbo.Address a
                            ON c.addressId = a.Id";

            List<CustomerAddress> customers = CustomerMock.CustomerAddressModelFaker.Generate(6);

            MockRepositoryBuilder.GetMockDbConnection().SetupDapperAsync(c => c.QueryAsync<CustomerAddress>(
                query,
                null,
                null,
                null,
                null)).ReturnsAsync(customers);

            var response = await _httpClient.GetAsync("/api/v1/customers");



            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetAll_HttpStatusCodeUnauthorizedTestAsync()
        {
            MockAuthorizationFactory.AddAnonymousHeaders(_httpClient);

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
        public async Task Post_HttpStatusCodeOkTestAsync()
        {
            MockAuthorizationFactory.AddAdminHeaders(_httpClient);

            var customer = CustomerMock.CustomerViewModelFaker.Generate();

            var query = @"SELECT c.Id AS id, a.Id AS addressId, c.Name AS name, c.DateCreated AS dateCreated, a.CEP AS cep
                          FROM dbo.Customer c
                          INNER JOIN dbo.Address a
                          ON c.addressId = a.Id
                          WHERE c.Name = @Name";

            var customerDb = CustomerMock.CustomerAddressModelFaker.Generate(1);


            MockRepositoryBuilder.GetMockDbConnection().SetupDapperAsync(c => c.QueryAsync<CustomerAddress>(
                query,
                It.IsAny<object>(),
                null,
                null,
                null)).ReturnsAsync(()=> new List<CustomerAddress>());


            var response = await _httpClient.PostAsync("/api/v1/customers",
                new StringContent(JsonConvert.SerializeObject(customer),
                Encoding.UTF8,
                "application/json"));

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }


        [Fact]
        public async Task Post_HttpStatusCodeBadRequestTestAsync()
        {
            MockAuthorizationFactory.AddAdminHeaders(_httpClient);

            var customer = CustomerMock.CustomerViewModelFaker.Generate();

            var response = await _httpClient.PostAsync("/api/v1/customers", new StringContent(JsonConvert.SerializeObject(customer)));

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
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
