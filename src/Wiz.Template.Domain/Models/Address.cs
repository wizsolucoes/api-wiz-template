using System.Collections.Generic;

namespace Wiz.Template.Domain.Models
{
    public class Address
    {
        protected Address()
        {
            Customers = new HashSet<Customer>();
        }

        public Address(string cep)
        {
            CEP = cep;
        }

        public int Id { get; private set; }
        public string CEP { get; private set; }

        public ICollection<Customer> Customers { get; private set; }
    }
}
