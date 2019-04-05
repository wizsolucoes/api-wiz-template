using Bogus;
using System;
using System.Collections.Generic;
using Wiz.Template.Domain.Models;
using Wiz.Template.Domain.Models.Dapper;

namespace Wiz.Template.Tests.Mocks.Models
{
    public static class CustomerMock
    {
        private readonly static Faker _faker = new Faker();

        public static Customer GetCustomer()
        {
            return new Customer(
                id: 1,
                addressId: _faker.Random.Number(),
                name: _faker.Name.FullName(Bogus.DataSets.Name.Gender.Male)
            );
        }

        public static CustomerAddress GetCustomerAddress()
        {
            return new CustomerAddress(
                id: 1,
                addressId: _faker.Random.Number(),
                name: _faker.Name.FullName(Bogus.DataSets.Name.Gender.Male),
                dateCreated: DateTime.Now,
                cep: _faker.Address.ZipCode()
            );
        }

        public static IEnumerable<CustomerAddress> GetCustomersAddress()
        {
            return new List<CustomerAddress>()
            {
                GetCustomerAddress(),
                GetCustomerAddress(),
                GetCustomerAddress()
            };
        }
    }
}
