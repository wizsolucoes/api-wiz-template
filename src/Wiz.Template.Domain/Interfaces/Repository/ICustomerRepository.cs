using System.Collections.Generic;
using System.Threading.Tasks;
using Wiz.Template.Domain.Models.Dapper;
using Wizco.Common.DataAccess;

namespace Wiz.Template.Domain.Interfaces.Repository;

public interface ICustomerRepository : IRepository
{
    Task<IEnumerable<CustomerAddress>> GetAllAsync();

    Task<CustomerAddress> GetAddressByIdAsync(int id);

    Task<CustomerAddress> GetByNameAsync(string name);
}
