using System.Collections.Generic;
using System.Linq;
using Wiz.Template.Domain.Models;

namespace Wiz.Template.Infra.Context
{
    public class EntityContextSeed
    {
        private readonly EntityContext _context;

        public EntityContextSeed(EntityContext context)
        {
            this._context = context;
            this.SeedInitial();
        }

        public void SeedInitial()
        {
            if (!_context.Addresses.Any())
            {
                var addresses = new List<Address>()
                {
                    new Address().AddCep("17052520"),
                    new Address().AddCep("44573100"),
                    new Address().AddCep("50080490")
                };

                _context.AddRange(addresses);
                _context.SaveChanges();
            }

            if (!_context.Customers.Any())
            {
                var addresses = _context.Addresses.ToList();

                _context.Add(new Customer(addressId: addresses.First(x => x.CEP == "17052520").Id, name: "Zier Zuveiku"));
                _context.Add(new Customer(addressId: addresses.First(x => x.CEP == "44573100").Id, name: "Vikehel Pleamakh"));
                _context.Add(new Customer(addressId: addresses.First(x => x.CEP == "50080490").Id, name: "Diuor PleaBolosmakh"));
                _context.Add(new Customer(addressId: addresses.First(x => x.CEP == "44573100").Id, name: "Fulano"));
                _context.Add(new Customer(addressId: addresses.First(x => x.CEP == "50080490").Id, name: "Fulano Beltrano"));
                _context.Add(new Customer(addressId: addresses.First(x => x.CEP == "17052520").Id, name: "Diuor PleaBolosmakh"));
                _context.Add(new Customer(addressId: addresses.First(x => x.CEP == "44573100").Id, name: "Luiz Costa Alves"));
                _context.Add(new Customer(addressId: addresses.First(x => x.CEP == "50080490").Id, name: "Diuor PleaBolosmakh"));
                _context.Add(new Customer(addressId: addresses.First(x => x.CEP == "44573100").Id, name: "Diuor PleaBolosmakh"));
                _context.Add(new Customer(addressId: addresses.First(x => x.CEP == "17052520").Id, name: "Harold P. Long"));
                _context.Add(new Customer(addressId: addresses.First(x => x.CEP == "50080490").Id, name: "Diuor PleaBolosmakh"));

                int i = _context.SaveChanges();
            }
        }
    }
}
