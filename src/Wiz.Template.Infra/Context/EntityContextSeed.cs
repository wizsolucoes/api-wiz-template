using System.Collections.Generic;
using System.Linq;
using Wiz.Template.Domain.Models;

namespace Wiz.Template.Infra.Context
{
    public class EntityContextSeed
    {
        public void SeedInitial(EntityContext context)
        {
            if (!context.Addresses.Any())
            {
                var addresses = new List<Address>()
                {
                    new Address().AddCep("17052520"),
                    new Address().AddCep("17052520"),
                    new Address().AddCep("17052520")
                };

                context.AddRange(addresses);
                context.SaveChanges();
            }

            if (!context.Customers.Any())
            {
                var addresses = context.Addresses.ToList();

                var customers = new List<Customer>()
                {
                    new Customer(id: 0, addressId: addresses.First(x => x.CEP == "17052520").Id, name: "Zier Zuveiku"),
                    new Customer(id: 0, addressId: addresses.First(x => x.CEP == "44573100").Id, name: "Vikehel Pleamakh"),
                    new Customer(id: 0, addressId: addresses.First(x => x.CEP == "50080490").Id, name: "Diuor PleaBolosmakh")
                };

                context.AddRange(customers);
                context.SaveChanges();
            }
        }
    }
}
