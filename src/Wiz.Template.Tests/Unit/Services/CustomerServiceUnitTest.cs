using AutoMapper;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Wiz.Template.API.Services;
using Wiz.Template.API.ViewModels.Customer;
using Wiz.Template.Domain.Interfaces.Notifications;
using Wiz.Template.Domain.Interfaces.Repository;
using Wiz.Template.Domain.Interfaces.Services;
using Wiz.Template.Domain.Interfaces.UoW;
using Wiz.Template.Domain.Models;
using Wiz.Template.Domain.Models.Dapper;
using Wiz.Template.Tests.Mocks.Models;
using Wiz.Template.Tests.Mocks.Models.Services;
using Wiz.Template.Tests.Mocks.ViewModels;
using Xunit;

namespace Wiz.Template.Tests.Unit.Services
{
    public class CustomerServiceUnitTest
    {
        private readonly Mock<ICustomerRepository> _customerRepositoryMock;
        private readonly Mock<IViaCEPService> _viaCEPServiceMock;
        private readonly Mock<IDomainNotification> _domainNotificationMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IMapper> _mapperMock;

        public CustomerServiceUnitTest()
        {
            _customerRepositoryMock = new Mock<ICustomerRepository>();
            _viaCEPServiceMock = new Mock<IViaCEPService>();
            _domainNotificationMock = new Mock<IDomainNotification>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _mapperMock = new Mock<IMapper>();
        }

        [Fact]
        public async Task GetAll_ReturnCustomerAddressViewModelTestAsync()
        {
            var cep = "17052520";

            _customerRepositoryMock.Setup(x => x.GetAllAsync())
                .ReturnsAsync(CustomerMock.GetCustomersAddress());

            _mapperMock.Setup(x => x.Map<IEnumerable<CustomerAddressViewModel>>(It.IsAny<IEnumerable<CustomerAddress>>()))
                .Returns(CustomerViewModelMock.GetCustomersAddress());

            _viaCEPServiceMock.Setup(x => x.GetByCEPAsync(cep))
                .ReturnsAsync(ViaCEPMock.GetCEP());

            var customerService = new CustomerService(_customerRepositoryMock.Object,
                _viaCEPServiceMock.Object, _domainNotificationMock.Object,
                _unitOfWorkMock.Object, _mapperMock.Object);

            var customeMethod = await customerService.GetAllAsync();

            var customerResult = Assert.IsAssignableFrom<IEnumerable<CustomerAddressViewModel>>(customeMethod);

            Assert.NotNull(customerResult);
            Assert.NotEmpty(customerResult);
        }

        [Fact]
        public async Task GetById_ReturnCustomerViewModelTestAsync()
        {
            int id = 1;
            var customerId = CustomerViewModelMock.GetCustomerId(id);

            _customerRepositoryMock.Setup(x => x.GetByIdAsync(id))
                .ReturnsAsync(CustomerMock.GetCustomer());

            _mapperMock.Setup(x => x.Map<CustomerViewModel>(It.IsAny<Customer>()))
                .Returns(CustomerViewModelMock.GetCustomer());

            var customerService = new CustomerService(_customerRepositoryMock.Object,
                _viaCEPServiceMock.Object, _domainNotificationMock.Object,
                _unitOfWorkMock.Object, _mapperMock.Object);

            var customeMethod = await customerService.GetByIdAsync(customerId);

            var customerResult = Assert.IsAssignableFrom<CustomerViewModel>(customeMethod);

            Assert.NotNull(customerResult);
        }

        [Fact]
        public async Task GetAddressByIdAsync_ReturnCustomerAddressViewModelTestAsync()
        {
            int id = 1;
            var customerId = CustomerViewModelMock.GetCustomerId(id);

            _customerRepositoryMock.Setup(x => x.GetAddressByIdAsync(id))
                .ReturnsAsync(CustomerMock.GetCustomerAddress());

            _mapperMock.Setup(x => x.Map<CustomerAddressViewModel>(It.IsAny<CustomerAddress>()))
                .Returns(CustomerViewModelMock.GetCustomerAddress());

            var customerService = new CustomerService(_customerRepositoryMock.Object,
                _viaCEPServiceMock.Object, _domainNotificationMock.Object,
                _unitOfWorkMock.Object, _mapperMock.Object);

            var customeMethod = await customerService.GetAddressByIdAsync(customerId);

            var customerResult = Assert.IsAssignableFrom<CustomerAddressViewModel>(customeMethod);

            Assert.NotNull(customerResult);
        }

        [Fact]
        public async Task GetAddressByNameAsync_ReturnCustomerAddressViewModelTestAsync()
        {
            var name = "Diuor PleaBolosmakh";
            var customerName = CustomerViewModelMock.GetCustomerName(name);

            _customerRepositoryMock.Setup(x => x.GetByNameAsync(name))
                .ReturnsAsync(CustomerMock.GetCustomerAddress());

            _mapperMock.Setup(x => x.Map<CustomerAddressViewModel>(It.IsAny<CustomerAddress>()))
                .Returns(CustomerViewModelMock.GetCustomerAddress());

            var customerService = new CustomerService(_customerRepositoryMock.Object,
                _viaCEPServiceMock.Object, _domainNotificationMock.Object,
                _unitOfWorkMock.Object, _mapperMock.Object);

            var customeMethod = await customerService.GetAddressByNameAsync(customerName);

            var customerResult = Assert.IsAssignableFrom<CustomerAddressViewModel>(customeMethod);

            Assert.NotNull(customerResult);
        }

        [Fact]
        public void Add_ReturnCustomerViewModelTestAsync()
        {
            var customer = CustomerViewModelMock.GetCustomer();

            _mapperMock.Setup(x => x.Map<Customer>(It.IsAny<CustomerViewModel>()))
                .Returns(CustomerMock.GetCustomer());

            _mapperMock.Setup(x => x.Map<CustomerViewModel>(It.IsAny<Customer>()))
                .Returns(CustomerViewModelMock.GetCustomer());

            _customerRepositoryMock.Setup(x => x.GetByNameAsync(customer.Name))
                .ReturnsAsync(CustomerMock.GetCustomerAddress());

            var customerService = new CustomerService(_customerRepositoryMock.Object,
                _viaCEPServiceMock.Object, _domainNotificationMock.Object,
                _unitOfWorkMock.Object, _mapperMock.Object);

            customerService.Add(customer);

            Assert.NotNull(customer);
        }

        [Fact]
        public void Update_SucessTestAsync()
        {
            var customer = CustomerViewModelMock.GetCustomer();

            _mapperMock.Setup(x => x.Map<Customer>(It.IsAny<CustomerViewModel>()))
                .Returns(CustomerMock.GetCustomer());

            var customerService = new CustomerService(_customerRepositoryMock.Object,
                _viaCEPServiceMock.Object, _domainNotificationMock.Object,
                _unitOfWorkMock.Object, _mapperMock.Object);

            customerService.Update(customer);

            Assert.NotNull(customer);
        }

        [Fact]
        public void Remove_SucessTestAsync()
        {
            var customer = CustomerViewModelMock.GetCustomer();

            _mapperMock.Setup(x => x.Map<Customer>(It.IsAny<CustomerViewModel>()))
                .Returns(CustomerMock.GetCustomer());

            var customerService = new CustomerService(_customerRepositoryMock.Object,
                _viaCEPServiceMock.Object, _domainNotificationMock.Object,
                _unitOfWorkMock.Object, _mapperMock.Object);

            customerService.Remove(customer);

            Assert.NotNull(customer);
        }
    }
}
