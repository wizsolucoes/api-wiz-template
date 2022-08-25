using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using Wiz.Template.API.Services.Interfaces;
using Wiz.Template.API.ViewModels.Customer;
using Wiz.Template.Domain.Interfaces.Notifications;
using Wiz.Template.Domain.Interfaces.Repository;
using Wiz.Template.Domain.Interfaces.Services;
using Wiz.Template.Domain.Interfaces.UoW;
using Wiz.Template.Domain.Models;
using Wiz.Template.Domain.Validation.CustomerValidation;

namespace Wiz.Template.API.Services;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IViaCEPService _viaCEPService;
    private readonly IDomainNotification _domainNotification;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CustomerService(ICustomerRepository customerRepository, IViaCEPService viaCEPService, IDomainNotification domainNotification, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _customerRepository = customerRepository;
        _viaCEPService = viaCEPService;
        _domainNotification = domainNotification;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CustomerAddressViewModel>> GetAllAsync()
    {
        var customers = _mapper.Map<IEnumerable<CustomerAddressViewModel>>(await _customerRepository.GetAllAsync());

        foreach (var customer in customers)
        {
            var address = await _viaCEPService.GetByCEPAsync(customer.CEP);
            customer.Address.Id = customer.AddressId;
            customer.Address.Street = address?.Street;
            customer.Address.StreetFull = address?.StreetFull;
            customer.Address.UF = address?.UF;
        }
        return customers;
    }

    public async Task<CustomerViewModel> GetByIdAsync(CustomerIdViewModel customerVM)
    {
        return _mapper.Map<CustomerViewModel>(await _customerRepository.GetByIdAsync(customerVM.Id));
    }

    public async Task<CustomerAddressViewModel> GetAddressByIdAsync(CustomerIdViewModel customerVM)
    {
        var teste =  await _customerRepository.GetAddressByIdAsync(customerVM.Id);
        var customer = _mapper.Map<CustomerAddressViewModel>(teste);

        if (customer != null)
        {
            var address = await _viaCEPService.GetByCEPAsync(customer.CEP);

            customer.Address.Id = customer.AddressId;
            customer.Address.Street = address?.Street;
            customer.Address.StreetFull = address?.StreetFull;
            customer.Address.UF = address?.UF;
        }

        return customer;
    }

    public async Task<CustomerAddressViewModel> GetAddressByNameAsync(CustomerNameViewModel customerVM)
    {
        var customer = _mapper.Map<CustomerAddressViewModel>(await _customerRepository.GetByNameAsync(customerVM.Name));

        if (customer != null)
        {
            var address = await _viaCEPService.GetByCEPAsync(customer.CEP);

            customer.Address.Id = customer.AddressId;
            customer.Address.Street = address?.Street;
            customer.Address.StreetFull = address?.StreetFull;
            customer.Address.UF = address?.UF;
        }

        return customer;
    }

    public async Task<CustomerViewModel> AddAsync(CustomerViewModel customerVM)
    {
        CustomerViewModel viewModel = null;
        var model = _mapper.Map<Customer>(customerVM);

        var validation = await new CustomerInsertValidation(_customerRepository).ValidateAsync(model);

        if (!validation.IsValid)
        {
            _domainNotification.AddNotifications(validation);
            return viewModel;
        }

        /*
         * EXEMPLO COM TRANSAÇÃO: 
         * Adicione a função "BeginTransaction()": _unitOfWork.BeginTransaction();
         * Utilize transação somente se realizar mais de uma operação no banco de dados ou banco de dados distintos
        */

        _customerRepository.Add(model);
        _unitOfWork.Commit();

        viewModel = _mapper.Map<CustomerViewModel>(model);

        return viewModel;
    }

    public async Task UpdateAsync(CustomerViewModel customerVM)
    {
        var model = _mapper.Map<Customer>(customerVM);

        var validation = await new CustomerUpdateValidation(_customerRepository).ValidateAsync(model);

        if (!validation.IsValid)
        {
            _domainNotification.AddNotifications(validation);
            return;
        }

        _customerRepository.Update(model);
        _unitOfWork.Commit();
    }

    public async Task RemoveAsync(CustomerViewModel customerVM)
    {
        var model = _mapper.Map<Customer>(customerVM);

        var validation = await new CustomerDeleteValidation().ValidateAsync(model);

        if (!validation.IsValid)
        {
            _domainNotification.AddNotifications(validation);
            return;
        }

        _customerRepository.Remove(model);
        _unitOfWork.Commit();
    }
}
