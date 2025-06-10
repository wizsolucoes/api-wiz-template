using System.Threading.Tasks;
using Wizco.Common.Base;

namespace Wiz.Template.Domain.Interfaces.Repository;

public interface IMerchantRepository : IRepository
{
    Task<bool> ExistsByIdAsync(int merchantId);
}
