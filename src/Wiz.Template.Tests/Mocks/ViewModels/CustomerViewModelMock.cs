using System;
using System.Collections.Generic;
using Bogus;
using Wiz.Template.API.ViewModels.Customer;

namespace Wiz.Template.Tests.Mocks.ViewModels
{
    public class CustomerViewModelMock
    {
        private readonly static Faker _faker = new Faker();

        public static CustomerViewModel GetCustomer()
        {
            return new CustomerViewModel(
                id: 1,
                addressId: _faker.Random.Number(),
                name: _faker.Name.FullName(Bogus.DataSets.Name.Gender.Male)
            );
        }

        public static CustomerAddressViewModel GetCustomerAddress()
        {
            return new CustomerAddressViewModel(
                id: 1,
                addressId: _faker.Random.Number(),
                name: _faker.Name.FullName(Bogus.DataSets.Name.Gender.Male),
                dateCreated: DateTime.Now,
                cep: _faker.Address.ZipCode()
            );
        }

        public static CustomerIdViewModel GetCustomerId(int id)
        {
            return new CustomerIdViewModel(id);
        }

        public static CustomerNameViewModel GetCustomerName(string name)
        {
            return new CustomerNameViewModel(name);
        }

        public static IEnumerable<CustomerAddressViewModel> GetCustomersAddress()
        {
            return new List<CustomerAddressViewModel>()
            {
                GetCustomerAddress(),
                GetCustomerAddress(),
                GetCustomerAddress()
            };
        }
    }
}
