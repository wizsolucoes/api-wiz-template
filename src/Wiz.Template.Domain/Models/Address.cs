using System.Collections.Generic;

namespace Wiz.Template.Domain.Models;

public class Address
{
    public Address()
    {
        Customers = new HashSet<Customer>();
    }

    public int Id { get; private set; }
    public string CEP { get; private set; }

    public ICollection<Customer> Customers { get; private set; }

    public Address AddCep(string cep){
        this.CEP = cep;
        return this;
    }
}
