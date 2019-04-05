using System.Collections.Generic;
using System.Threading.Tasks;
using Wiz.Template.Domain.Models;
using Wiz.Template.Domain.Models.Dapper;

namespace Wiz.Template.Domain.Interfaces.Repository
{
    public interface ICustomerRepository : IEntityBaseRepository<Customer>, IDapperReadRepository<Customer>
    {
        Task<IEnumerable<CustomerAddress>> GetAllAsync();
        Task<CustomerAddress> GetAddressByIdAsync(int id);
        Task<CustomerAddress> GetByNameAsync(string name);
    }
}
