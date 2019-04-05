using System;

namespace Wiz.Template.Domain.Models.Dapper
{
    public class CustomerAddress
    {
        public CustomerAddress(int id, int addressId, string name, DateTime dateCreated, string cep)
        {
            Id = id;
            AddressId = addressId;
            Name = name;
            DateCreated = dateCreated;
            CEP = cep;
        }

        public int Id { get; set; }
        public int AddressId { get; set; }
        public string Name { get; set; }
        public DateTime DateCreated { get; set; }
        public string CEP { get; set; }
    }
}
