using Moq;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Wiz.Template.API;
using Wiz.Template.Core.Tests.Mocks;
using Wiz.Template.Core.Tests.Mocks.Factory;
using Wiz.Template.Domain.Models.Dapper;
using Xunit;
using System.Collections.Generic;
using Dapper;
using System.Text;
using Moq.Dapper;
using Wiz.Template.Core.Tests.Fixture;
using Wiz.Template.Domain.Models;
using Wiz.Template.API.ViewModels.Customer;
using NJsonSchema;
using Wiz.Template.Infra.Context;



namespace Wiz.Template.Integration.Tests.API
{
    public class CustomerControllerTest : IClassFixture<WebApplicationFixture<Startup>>
    {
        private readonly HttpClient _httpClient;

        public CustomerControllerTest(WebApplicationFixture<Startup> factory)
        {
            _httpClient = factory.CreateClient();
            this.Arrange();
            SqlMapper.PurgeQueryCache();
        }

        private void Arrange()
        {
            var queryCustomers = @"SELECT c.Id AS id, a.Id AS addressId, c.Name AS name, c.DateCreated AS dateCreated, a.CEP AS cep
                            FROM dbo.Customer c
                            INNER JOIN dbo.Address a
                            ON c.addressId = a.Id";

            List<CustomerAddress> customers = CustomerMock.CustomerAddressModelFaker.Generate(6);

            MockRepositoryBuilder.GetMockDbConnection().SetupDapperAsync(c => c.QueryAsync<CustomerAddress>(
                queryCustomers,
                null,
                null,
                null,
                null)).ReturnsAsync(customers);


            var queryCustomerAddressByName = @"SELECT c.Id AS id, a.Id AS addressId, c.Name AS name, c.DateCreated AS dateCreated, a.CEP AS cep
                          FROM dbo.Customer c
                          INNER JOIN dbo.Address a
                          ON c.addressId = a.Id
                          WHERE c.Name = @Name";

            MockRepositoryBuilder.GetMockDbConnection().SetupDapperAsync(c => c.QueryAsync<CustomerAddress>(
                queryCustomerAddressByName,
                It.IsAny<object>(),
                null,
                null,
                null)).ReturnsAsync(() => new List<CustomerAddress>());



            var queryCustomerById = @"SELECT Id, AddressId, Name, DateCreated
                          FROM dbo.Customer c
                          WHERE c.Id = @Id";

            MockRepositoryBuilder.GetMockDbConnection().SetupDapperAsync(c => c.QueryAsync<Customer>(
                queryCustomerById,
                It.IsAny<object>(),
                null,
                null,
                null)).ReturnsAsync(new List<Customer>());

        }

        #region [ 200 Ok Test ]
        [Fact]
        public async Task GetAll_OKTestAsync()
        {
            MockAuthorizationFactory.AddAdminHeaders(_httpClient);

            var response = await _httpClient.GetAsync("/api/v1/customers");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }


        [Fact]
        public async Task GetById_OKTestAsync()
        {
            var queryCustomerAddressById = @"SELECT c.Id, a.Id AS AddressId, c.Name, c.DateCreated, a.CEP
                          FROM dbo.Customer c
                          INNER JOIN dbo.Address a
                          ON c.AddressId = a.Id
                          WHERE c.Id = @Id";

            List<CustomerAddress> customerAddressById = CustomerMock.CustomerAddressModelFaker.Generate(1);

            MockRepositoryBuilder.GetMockDbConnection().SetupDapperAsync(c => c.QueryAsync<CustomerAddress>(
                queryCustomerAddressById,
                It.IsAny<object>(),
                null,
                null,
                null)).ReturnsAsync(customerAddressById);

            MockAuthorizationFactory.AddAdminHeaders(_httpClient);

            var response = await _httpClient.GetAsync("/api/v1/customers/1");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetByName_OKTestAsync()
        {
            var queryCustomerAddressByName = @"SELECT c.Id AS id, a.Id AS addressId, c.Name AS name, c.DateCreated AS dateCreated, a.CEP AS cep
                          FROM dbo.Customer c
                          INNER JOIN dbo.Address a
                          ON c.addressId = a.Id
                          WHERE c.Name = @Name";

            List<CustomerAddress> customerAddressByName = CustomerMock.CustomerAddressModelFaker.Generate(1);

            MockRepositoryBuilder.GetMockDbConnection().SetupDapperAsync(c => c.QueryAsync<CustomerAddress>(
                queryCustomerAddressByName,
                It.IsAny<object>(),
                null,
                null,
                null)).ReturnsAsync(customerAddressByName);

            MockAuthorizationFactory.AddAdminHeaders(_httpClient);

            var response = await _httpClient.GetAsync("/api/v1/customers/name/joao");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        #endregion

        #region [ 201 Created Test ]

        [Fact]
        public async Task PostCustomer_CreatedTestAsync()
        {
            MockAuthorizationFactory.AddAdminHeaders(_httpClient);
            var customer = CustomerMock.CustomerViewModelFaker.Generate();
            customer.Id = 0;

            var response = await _httpClient.PostAsync("/api/v1/customers",
                new StringContent(JsonConvert.SerializeObject(customer),
                Encoding.UTF8,
                "application/json"));

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        #endregion

        #region [ 202 Accepted Test ]

        [Fact]
        public async Task PutCustomer_AcceptedTestAsync()
        {
            var queryCustomerById = @"SELECT Id, AddressId, Name, DateCreated
                          FROM dbo.Customer c
                          WHERE c.Id = @Id";

            List<Customer> customerModel = CustomerMock.CustomerModelFaker.Generate(1);

            MockRepositoryBuilder.GetMockDbConnection().SetupDapperAsync(c => c.QueryAsync<Customer>(
                queryCustomerById,
                It.IsAny<object>(),
                null,
                null,
                null)).ReturnsAsync(customerModel);

            MockAuthorizationFactory.AddAdminHeaders(_httpClient);

            var customer = CustomerMock.CustomerViewModelFaker.Generate();
            customer.Id = customerModel[0].Id;

            var response = await _httpClient.PutAsync($"/api/v1/customers/{customerModel[0].Id}",
                new StringContent(JsonConvert.SerializeObject(customer),
                Encoding.UTF8,
                "application/json"));


            Assert.Equal(HttpStatusCode.Accepted, response.StatusCode);
        }


        [Fact]
        public async Task DeleteCustomer_AcceptedTestAsync()
        {
            var queryCustomerById = @"SELECT Id, AddressId, Name, DateCreated
                          FROM dbo.Customer c
                          WHERE c.Id = @Id";

            List<Customer> customerModel = CustomerMock.CustomerModelFaker.Generate(1);

            MockRepositoryBuilder.GetMockDbConnection().SetupDapperAsync(c => c.QueryAsync<Customer>(
                queryCustomerById,
                It.IsAny<object>(),
                null,
                null,
                null)).ReturnsAsync(customerModel);

            MockAuthorizationFactory.AddAdminHeaders(_httpClient);

            var customer = CustomerMock.CustomerViewModelFaker.Generate();
            customer.Id = customerModel[0].Id;

            var response = await _httpClient.DeleteAsync($"/api/v1/customers/{customerModel[0].Id}");

            Assert.Equal(HttpStatusCode.Accepted, response.StatusCode);
        }

        #endregion

        #region [ 204 NoContent Test ]

        [Theory]
        [InlineData("/api/v1/customers/1", "Put")]
        public async Task Put_NoContentTestAsync(string endpoint, string method)
        {
            MockAuthorizationFactory.AddAdminHeaders(this._httpClient);

            var customer = CustomerMock.CustomerViewModelFaker.Generate();
            customer.Id = 1;

            HttpRequestMessage request = new HttpRequestMessage
            {
                RequestUri = new System.Uri($"{this._httpClient.BaseAddress.OriginalString}{endpoint}"),
                Method = new HttpMethod(method),
                Headers = { { "User-Agent", "csharp" } },
                Content = new StringContent(JsonConvert.SerializeObject(customer),
                    Encoding.UTF8,
                "application/json")
            };

            var response = await this._httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead);

            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }


        [Theory]
        [InlineData("/api/v1/customers/1", "Get")]
        [InlineData("/api/v1/customers/name/joao", "Get")]
        [InlineData("/api/v1/customers", "Post")]
        [InlineData("/api/v1/customers/1", "Delete")]
        public async Task Generic_NoContentTestAsync(string endpoint, string method)
        {
            MockAuthorizationFactory.AddAdminHeaders(this._httpClient);

            HttpRequestMessage request = new HttpRequestMessage
            {
                RequestUri = new System.Uri($"{this._httpClient.BaseAddress.OriginalString}{endpoint}"),
                Method = new HttpMethod(method),
                Headers = { { "User-Agent", "csharp" } },
                Content = new StringContent("{}",
                    Encoding.UTF8,
                    "application/json")
            };

            var response = await this._httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead);

            string message = await response.Content.ReadAsStringAsync();

            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

        #endregion

        #region [ 400 BadRequest Test ]

        [Fact]
        public async Task PostCustomer_BadRequesTestAsync()
        {
            var queryCustomerAddressByName = @"SELECT c.Id AS id, a.Id AS addressId, c.Name AS name, c.DateCreated AS dateCreated, a.CEP AS cep
                          FROM dbo.Customer c
                          INNER JOIN dbo.Address a
                          ON c.addressId = a.Id
                          WHERE c.Name = @Name";

            List<CustomerAddress> customerAddressByName = CustomerMock.CustomerAddressModelFaker.Generate(1);

            MockRepositoryBuilder.GetMockDbConnection().SetupDapperAsync(c => c.QueryAsync<CustomerAddress>(
                queryCustomerAddressByName,
                It.IsAny<object>(),
                null,
                null,
                null)).ReturnsAsync(customerAddressByName);

            MockAuthorizationFactory.AddAdminHeaders(_httpClient);
            var customer = CustomerMock.CustomerViewModelFaker.Generate();
            customer.Id = customerAddressByName[0].Id;
            customer.Name = customerAddressByName[0].Name;

            HttpRequestMessage request = new HttpRequestMessage
            {
                RequestUri = new System.Uri($"{this._httpClient.BaseAddress.OriginalString}/api/v1/customers"),
                Method = new HttpMethod("Post"),
                Headers = { { "User-Agent", "csharp" } },
                Content = new StringContent(JsonConvert.SerializeObject(customer),
                    Encoding.UTF8,
                    "application/json")
            };

            var response = await this._httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead);


            string message = await response.Content.ReadAsStringAsync();

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }


        [Theory]
        [InlineData("/api/v1/customers/a", "Get")]
        [InlineData("/api/v1/customers", "Post")]
        [InlineData("/api/v1/customers/1", "Put")]
        [InlineData("/api/v1/customers/a", "Delete")]
        public async Task Generic_BadRequestTestAsync(string endpoint, string method)
        {
            MockAuthorizationFactory.AddAdminHeaders(this._httpClient);

            HttpRequestMessage request = new HttpRequestMessage
            {
                RequestUri = new System.Uri($"{this._httpClient.BaseAddress.OriginalString}{endpoint}"),
                Method = new HttpMethod(method),
                Headers = { { "User-Agent", "csharp" } },
                Content = new StringContent("",
                    Encoding.UTF8,
                    "application/text")
            };

            var response = await this._httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead);

            string message = await response.Content.ReadAsStringAsync();

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        #endregion

        #region [ 401 Unauthorized Test ]

        [Theory]
        [InlineData("/api/v1/customers", "Get")]
        [InlineData("/api/v1/customers/1", "Get")]
        [InlineData("/api/v1/customers/name/joao", "Get")]
        [InlineData("/api/v1/customers", "Post")]
        [InlineData("/api/v1/customers/1", "Put")]
        [InlineData("/api/v1/customers/1", "Delete")]
        public async Task Generic_UnauthorizedTestAsync(string endpoint, string method)
        {
            MockAuthorizationFactory.AddAnonymousHeaders(this._httpClient);

            HttpRequestMessage request = new HttpRequestMessage
            {
                RequestUri = new System.Uri($"{this._httpClient.BaseAddress.OriginalString}{endpoint}"),
                Method = new HttpMethod(method),
                Headers = { { "User-Agent", "csharp" } },
                Content = new StringContent("{}",
                    Encoding.UTF8,
                    "application/json")
            };

            var response = await this._httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead);

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        #endregion
    }
}
