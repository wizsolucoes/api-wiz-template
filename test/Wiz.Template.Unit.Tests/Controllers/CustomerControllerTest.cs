using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Wiz.Template.API.Controllers;
using Wiz.Template.API.Services.Interfaces;
using Wiz.Template.API.ViewModels.Customer;
using Wiz.Template.Core.Tests.Mocks;
using Xunit;

namespace Wiz.Template.Unit.Tests.Controllers
{
    public class CustomerControllerTest
    {
        private readonly Mock<ICustomerService> _customerServiceMock;

        public CustomerControllerTest()
        {
            _customerServiceMock = new Mock<ICustomerService>();
        }

        [Fact]
        public async Task GetAll_SucessTestAsync()
        {
            _customerServiceMock.Setup(x => x.GetAllAsync()).ReturnsAsync(CustomerMock.CustomerAddressViewModelFaker.Generate(3));

            var customerController = new CustomerController(_customerServiceMock.Object);
            var customerService = await customerController.GetAll();

            var actionResult = Assert.IsType<OkObjectResult>(customerService.Result);
            var actionValue = Assert.IsAssignableFrom<IEnumerable<CustomerAddressViewModel>>(actionResult.Value);

            Assert.NotNull(actionResult);
            Assert.Equal(StatusCodes.Status200OK, actionResult.StatusCode);
        }

        [Fact]
        public async Task GetById_SucessTestAsync()
        {
            var customerId = CustomerMock.CustomerIdViewModelFaker.Generate();

            _customerServiceMock.Setup(x => x.GetAddressByIdAsync(customerId))
                .ReturnsAsync(CustomerMock.CustomerAddressViewModelFaker.Generate());

            var customerController = new CustomerController(_customerServiceMock.Object);
            var customerService = await customerController.GetById(customerId);

            var actionResult = Assert.IsType<OkObjectResult>(customerService.Result);
            var actionValue = Assert.IsType<CustomerAddressViewModel>(actionResult.Value);

            Assert.NotNull(actionResult);
            Assert.Equal(StatusCodes.Status200OK, actionResult.StatusCode);
        }

        [Fact]
        public async Task GetByName_SucessTestAsync()
        {
            var customerName = CustomerMock.CustomerNameViewModelFaker.Generate();

            _customerServiceMock.Setup(x => x.GetAddressByNameAsync(customerName))
                .ReturnsAsync(CustomerMock.CustomerAddressViewModelFaker.Generate());

            var customerController = new CustomerController(_customerServiceMock.Object);
            var customerService = await customerController.GetByName(customerName);

            var actionResult = Assert.IsType<OkObjectResult>(customerService.Result);
            var actionValue = Assert.IsType<CustomerAddressViewModel>(actionResult.Value);

            Assert.NotNull(actionResult);
            Assert.Equal(StatusCodes.Status200OK, actionResult.StatusCode);
        }

        [Fact]
        public void Post_SucessTestAsync()
        {
            var customer = CustomerMock.CustomerViewModelFaker.Generate();

            _customerServiceMock.Setup(x => x.Add(customer))
                .Returns(CustomerMock.CustomerViewModelFaker.Generate());

            var customerController = new CustomerController(_customerServiceMock.Object);
            var customerService = customerController.PostCustomer(customer);

            var actionResult = Assert.IsType<CreatedResult>(customerService.Result);
            var actionValue = Assert.IsType<CustomerViewModel>(actionResult.Value);

            Assert.NotNull(actionValue);
            Assert.Equal(StatusCodes.Status201Created, actionResult.StatusCode);
        }

        [Fact]
        public void Post_FailTestAsync()
        {
            CustomerViewModel customer = null;

            _customerServiceMock.Setup(x => x.Add(customer))
                .Returns(CustomerMock.CustomerViewModelFaker.Generate());

            var customerController = new CustomerController(_customerServiceMock.Object);
            var customerService = customerController.PostCustomer(customer);

            var actionResult = Assert.IsType<NotFoundResult>(customerService.Result);

            Assert.Equal(StatusCodes.Status404NotFound, actionResult.StatusCode);
        }

        [Fact]
        public async Task Put_BadRequestTestAsync()
        {
            var id = 1;
            CustomerViewModel customer = null;

            _customerServiceMock.Setup(x => x.Update(customer));

            var customerController = new CustomerController(_customerServiceMock.Object);
            var customerService = await customerController.PutCustomer(id, customer);

            var actionResult = Assert.IsType<BadRequestResult>(customerService);

            Assert.Equal(StatusCodes.Status400BadRequest, actionResult.StatusCode);
        }

        [Fact]
        public async Task Put_NotFoundTestAsync()
        {
            var customer = CustomerMock.CustomerViewModelFaker.Generate();

            _customerServiceMock.Setup(x => x.Update(customer));

            var customerController = new CustomerController(_customerServiceMock.Object);
            var customerService = await customerController.PutCustomer(customer.Id, customer);

            var actionResult = Assert.IsType<NotFoundResult>(customerService);

            Assert.Equal(StatusCodes.Status404NotFound, actionResult.StatusCode);
        }

        [Fact]
        public async Task Delete_SucessTestAsync()
        {
            var customerId = CustomerMock.CustomerIdViewModelFaker.Generate();
            var customer = CustomerMock.CustomerViewModelFaker.Generate();

            _customerServiceMock.Setup(x => x.GetByIdAsync(customerId))
                .ReturnsAsync(customer);

            _customerServiceMock.Setup(x => x.Remove(customer));

            var customerController = new CustomerController(_customerServiceMock.Object);
            var customerService = await customerController.DeleteCustomer(customerId);

            var actionResult = Assert.IsType<NoContentResult>(customerService);

            Assert.Equal(StatusCodes.Status204NoContent, actionResult.StatusCode);
        }

        [Fact]
        public async Task Delete_NotFoundTestAsync()
        {
            var customerId = CustomerMock.CustomerIdViewModelFaker.Generate();

            var customerController = new CustomerController(_customerServiceMock.Object);
            var customerService = await customerController.DeleteCustomer(customerId);

            var actionResult = Assert.IsType<NotFoundResult>(customerService);

            Assert.Equal(StatusCodes.Status404NotFound, actionResult.StatusCode);
        }
    }
}
