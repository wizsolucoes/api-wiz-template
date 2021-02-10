using System;

namespace Wiz.Template.Domain.Models
{
    public class Customer
    {
        protected Customer() { }

        public Customer(int id, int addressId, string name) : this(addressId, name)
        {
            Id = id;
        }

        public Customer(int addressId, string name)
        {
            AddressId = addressId;
            Name = name;
        }

        public int Id { get; private set; }
        public int AddressId { get; private set; }
        public string Name { get; private set; }
        public DateTime DateCreated { get; private set; }

        public Address Address { get; private set; }
    }
}
