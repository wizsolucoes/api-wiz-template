using Bogus;
using Wiz.Template.API.ViewModels.Customer;
using Wiz.Template.Domain.Models;
using Wiz.Template.Domain.Models.Dapper;

namespace Wiz.Template.Unit.Tests.Mocks
{
    public static class CustomerMock
    {
        public static Faker<CustomerAddress> CustomerAddressModelFaker =>
            new Faker<CustomerAddress>()
            .CustomInstantiator(x => new CustomerAddress
            (
                id: x.Random.Number(1, 10),
                addressId: x.Random.Number(1, 10),
                name: x.Person.FullName,
                dateCreated: x.Date.Future(),
                cep: x.Address.ZipCode()
            ));

        public static Faker<CustomerAddressViewModel> CustomerAddressViewModelFaker =>
            new Faker<CustomerAddressViewModel>()
            .CustomInstantiator(x => new CustomerAddressViewModel
            (
                id: x.Random.Number(1, 10),
                addressId: x.Random.Number(1, 10),
                name: x.Person.FullName,
                dateCreated: x.Date.Future(),
                cep: x.Address.ZipCode()
            ));

        public static Faker<Customer> CustomerModelFaker =>
            new Faker<Customer>()
            .CustomInstantiator(x => new Customer
            (
                id: x.Random.Number(1, 10),
                addressId: x.Random.Number(1, 10),
                name: x.Person.FullName
            ));

        public static Faker<CustomerViewModel> CustomerViewModelFaker =>
            new Faker<CustomerViewModel>()
            .CustomInstantiator(x => new CustomerViewModel
            (
                id: x.Random.Number(1, 10),
                addressId: x.Random.Number(1, 10),
                name: x.Person.FullName
            ));

        public static Faker<CustomerIdViewModel> CustomerIdViewModelFaker =>
            new Faker<CustomerIdViewModel>()
            .CustomInstantiator(x => new CustomerIdViewModel
            (
                id: x.Random.Number(1, 10)
            ));

        public static Faker<CustomerNameViewModel> CustomerNameViewModelFaker =>
            new Faker<CustomerNameViewModel>()
            .CustomInstantiator(x => new CustomerNameViewModel
            (
                name: x.Person.FullName
            ));
    }
}
