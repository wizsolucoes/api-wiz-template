using System;
using Wiz.Template.API.ViewModels.Address;

namespace Wiz.Template.API.ViewModels.Customer
{
    public class CustomerViewModel
    {
        public CustomerViewModel() { }

        public CustomerViewModel(int id, int addressId, string name)
        {
            Id = id;
            AddressId = addressId;
            Name = name;
        }

        public CustomerViewModel(int addressId, string name)
        {
            AddressId = addressId;
            Name = name;
        }

        public int Id { get; set; }
        public int AddressId { get; set; }
        public string Name { get; set; }
        public DateTime DateCreated { get; set; }
        public AddressViewModel Address { get; set; }
    }
}
