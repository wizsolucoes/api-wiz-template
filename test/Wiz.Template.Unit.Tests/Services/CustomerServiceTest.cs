using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Wiz.Template.API.Services;
using Wiz.Template.API.ViewModels.Customer;
using Wiz.Template.Core.Tests.Mocks;
using Wiz.Template.Domain.Interfaces.Notifications;
using Wiz.Template.Domain.Interfaces.Repository;
using Wiz.Template.Domain.Interfaces.Services;
using Wiz.Template.Domain.Interfaces.UoW;
using Wiz.Template.Unit.Tests.Configuration;
using Xunit;

namespace Wiz.Template.Unit.Tests.Services
{
    public class CustomerServiceTest : ConfigBase
    {
        private readonly Mock<ICustomerRepository> _customerRepositoryMock;
        private readonly Mock<IViaCEPService> _viaCEPServiceMock;
        private readonly Mock<IDomainNotification> _domainNotificationMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;

        public CustomerServiceTest()
        {
            _customerRepositoryMock = new Mock<ICustomerRepository>();
            _viaCEPServiceMock = new Mock<IViaCEPService>();
            _domainNotificationMock = new Mock<IDomainNotification>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
        }

        private CustomerService GetCustomerService()
        {
            return new CustomerService(_customerRepositoryMock.Object,
                            _viaCEPServiceMock.Object, _domainNotificationMock.Object,
                            _unitOfWorkMock.Object, _mapper);
        }

        [Fact]
        public async Task GetAll_ReturnCustomerAddressViewModelTestAsync()
        {
            var cep = "17052520";

            _customerRepositoryMock.Setup(x => x.GetAllAsync())
                .ReturnsAsync(CustomerMock.CustomerAddressModelFaker.Generate(3));

            _viaCEPServiceMock.Setup(x => x.GetByCEPAsync(cep))
                .ReturnsAsync(ViaCEPMock.ViaCEPModelFaker.Generate());

            var customerService = GetCustomerService();

            var customeMethod = await customerService.GetAllAsync();

            var customerResult = Assert.IsAssignableFrom<IEnumerable<CustomerAddressViewModel>>(customeMethod);

            Assert.NotNull(customerResult);
            Assert.NotEmpty(customerResult);
        }

        [Fact]
        public async Task GetById_ReturnCustomerViewModelTestAsync()
        {
            var customerId = CustomerMock.CustomerIdViewModelFaker.Generate();

            _customerRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(CustomerMock.CustomerModelFaker.Generate());

            var customerService = GetCustomerService();

            var customeMethod = await customerService.GetByIdAsync(customerId);

            var customerResult = Assert.IsAssignableFrom<CustomerViewModel>(customeMethod);

            Assert.NotNull(customerResult);
        }

        [Fact]
        public async Task GetAddressByIdAsync_ReturnCustomerAddressViewModelTestAsync()
        {
            var customerId = CustomerMock.CustomerIdViewModelFaker.Generate();

            _customerRepositoryMock.Setup(x => x.GetAddressByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(CustomerMock.CustomerAddressModelFaker.Generate());

            var customerService = GetCustomerService();

            var customeMethod = await customerService.GetAddressByIdAsync(customerId);

            var customerResult = Assert.IsAssignableFrom<CustomerAddressViewModel>(customeMethod);

            Assert.NotNull(customerResult);
        }

        [Fact]
        public async Task GetAddressByNameAsync_ReturnCustomerAddressViewModelTestAsync()
        {
            var customerName = CustomerMock.CustomerNameViewModelFaker.Generate();

            _customerRepositoryMock.Setup(x => x.GetByNameAsync(It.IsAny<string>()))
                .ReturnsAsync(CustomerMock.CustomerAddressModelFaker.Generate());

            var customerService = GetCustomerService();

            var customeMethod = await customerService.GetAddressByNameAsync(customerName);

            var customerResult = Assert.IsAssignableFrom<CustomerAddressViewModel>(customeMethod);

            Assert.NotNull(customerResult);
        }

        [Fact]
        public async Task Add_ReturnCustomerViewModelTestAsync()
        {
            var customer = CustomerMock.CustomerViewModelFaker.Generate();

            _customerRepositoryMock.Setup(x => x.GetByNameAsync(customer.Name))
                .ReturnsAsync(CustomerMock.CustomerAddressModelFaker.Generate());

            var customerService = GetCustomerService();

            await customerService.AddAsync(customer);

            Assert.NotNull(customer);
        }

        [Fact]
        public async Task Update_SucessTestAsync()
        {
            var customer = CustomerMock.CustomerViewModelFaker.Generate();

            var customerService = GetCustomerService();

            await customerService.UpdateAsync(customer);

            Assert.NotNull(customer);
        }

        [Fact]
        public async Task Remove_SucessTestAsync()
        {
            var customer = CustomerMock.CustomerViewModelFaker.Generate();

            var customerService = GetCustomerService();

            await customerService.RemoveAsync(customer);

            Assert.NotNull(customer);
        }
    }
}
