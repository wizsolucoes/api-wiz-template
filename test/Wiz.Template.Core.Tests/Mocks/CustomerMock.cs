using Bogus;
using Bogus.Extensions.Brazil;
using Wiz.Template.API.ViewModels.Customer;
using Wiz.Template.Domain.Models;
using Wiz.Template.Domain.Models.Dapper;

namespace Wiz.Template.Core.Tests.Mocks
{
    public static class CustomerMock
    {
        public static Faker<Customer> CustomerModelFaker =>
            new Faker<Customer>("pt_BR")
            .CustomInstantiator(x => new Customer
            (
                id: x.Random.Number(1, 10),
                addressId: x.Random.Number(1, 10),
                name: x.Person.FullName
            ));

        public static Faker<CustomerAddress> CustomerAddressModelFaker =>
            new Faker<CustomerAddress>("pt_BR")
            .CustomInstantiator(x => new CustomerAddress
            (

                id: x.Random.Number(1, 10),
                addressId: x.Random.Number(1, 10),
                name: x.Person.FullName,
                dateCreated: x.Date.Past(),
                cep: x.Address.ZipCode()
            ));

        public static Faker<CustomerViewModel> CustomerViewModelFaker =>
            new Faker<CustomerViewModel>("pt_BR")
            .CustomInstantiator(x => new CustomerViewModel
            (
                id: x.Random.Number(1, 10),
                addressId: x.Random.Number(1, 10),
                name: x.Person.FullName
            ));

        public static Faker<CustomerIdViewModel> CustomerIdViewModelFaker =>
            new Faker<CustomerIdViewModel>("pt_BR")
            .CustomInstantiator(x => new CustomerIdViewModel
            (
                id: x.Random.Number(1, 10)
            ));

        public static Faker<CustomerNameViewModel> CustomerNameViewModelFaker =>
            new Faker<CustomerNameViewModel>("pt_BR")
            .CustomInstantiator(x => new CustomerNameViewModel
            (
                name: x.Person.FullName
            ));

        public static Faker<CustomerAddressViewModel> CustomerAddressViewModelFaker =>
            new Faker<CustomerAddressViewModel>("pt_BR")
            .CustomInstantiator(x => new CustomerAddressViewModel
            (
                id: x.Random.Number(1, 10),
                addressId: x.Random.Number(1, 10),
                name: x.Person.FullName,
                dateCreated: x.Date.Past(),
                cep: x.Address.ZipCode(),
                address: AddressMock.AddressViewModelFaker
            ));

    }
}
