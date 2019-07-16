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
using Wiz.Template.Unit.Tests.Mocks;
using Xunit;

namespace Wiz.Template.Unit.Tests.Services
{
    public class CustomerServiceTest
    {
        private readonly Mock<ICustomerRepository> _customerRepositoryMock;
        private readonly Mock<IViaCEPService> _viaCEPServiceMock;
        private readonly Mock<IDomainNotification> _domainNotificationMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IMapper> _mapperMock;

        public CustomerServiceTest()
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
                .ReturnsAsync(CustomerMock.CustomerAddressModelFaker.Generate(3));

            _mapperMock.Setup(x => x.Map<IEnumerable<CustomerAddressViewModel>>(It.IsAny<IEnumerable<CustomerAddress>>()))
                .Returns(CustomerMock.CustomerAddressViewModelFaker.Generate(3));

            _viaCEPServiceMock.Setup(x => x.GetByCEPAsync(cep))
                .ReturnsAsync(ViaCEPMock.ViaCEPModelFaker.Generate());

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
            var customerId = CustomerMock.CustomerIdViewModelFaker.Generate();

            _customerRepositoryMock.Setup(x => x.GetByIdAsync(id))
                .ReturnsAsync(CustomerMock.CustomerModelFaker.Generate());

            _mapperMock.Setup(x => x.Map<CustomerViewModel>(It.IsAny<Customer>()))
                .Returns(CustomerMock.CustomerViewModelFaker.Generate());

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
            var customerId = CustomerMock.CustomerIdViewModelFaker.Generate();

            _customerRepositoryMock.Setup(x => x.GetAddressByIdAsync(id))
                .ReturnsAsync(CustomerMock.CustomerAddressModelFaker.Generate());

            _mapperMock.Setup(x => x.Map<CustomerAddressViewModel>(It.IsAny<CustomerAddress>()))
                .Returns(CustomerMock.CustomerAddressViewModelFaker.Generate());

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
            var customerName = CustomerMock.CustomerNameViewModelFaker.Generate();

            _customerRepositoryMock.Setup(x => x.GetByNameAsync(name))
                .ReturnsAsync(CustomerMock.CustomerAddressModelFaker.Generate());

            _mapperMock.Setup(x => x.Map<CustomerAddressViewModel>(It.IsAny<CustomerAddress>()))
                .Returns(CustomerMock.CustomerAddressViewModelFaker.Generate());

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
            var customer = CustomerMock.CustomerViewModelFaker.Generate();

            _mapperMock.Setup(x => x.Map<Customer>(It.IsAny<CustomerViewModel>()))
                .Returns(CustomerMock.CustomerModelFaker.Generate());

            _mapperMock.Setup(x => x.Map<CustomerViewModel>(It.IsAny<Customer>()))
                .Returns(CustomerMock.CustomerViewModelFaker.Generate());

            _customerRepositoryMock.Setup(x => x.GetByNameAsync(customer.Name))
                .ReturnsAsync(CustomerMock.CustomerAddressModelFaker.Generate());

            var customerService = new CustomerService(_customerRepositoryMock.Object,
                _viaCEPServiceMock.Object, _domainNotificationMock.Object,
                _unitOfWorkMock.Object, _mapperMock.Object);

            customerService.Add(customer);

            Assert.NotNull(customer);
        }

        [Fact]
        public void Update_SucessTestAsync()
        {
            var customer = CustomerMock.CustomerViewModelFaker.Generate();

            _mapperMock.Setup(x => x.Map<Customer>(It.IsAny<CustomerViewModel>()))
                .Returns(CustomerMock.CustomerModelFaker.Generate());

            var customerService = new CustomerService(_customerRepositoryMock.Object,
                _viaCEPServiceMock.Object, _domainNotificationMock.Object,
                _unitOfWorkMock.Object, _mapperMock.Object);

            customerService.Update(customer);

            Assert.NotNull(customer);
        }

        [Fact]
        public void Remove_SucessTestAsync()
        {
            var customer = CustomerMock.CustomerViewModelFaker.Generate();

            _mapperMock.Setup(x => x.Map<Customer>(It.IsAny<CustomerViewModel>()))
                .Returns(CustomerMock.CustomerModelFaker.Generate());

            var customerService = new CustomerService(_customerRepositoryMock.Object,
                _viaCEPServiceMock.Object, _domainNotificationMock.Object,
                _unitOfWorkMock.Object, _mapperMock.Object);

            customerService.Remove(customer);

            Assert.NotNull(customer);
        }
    }
}
