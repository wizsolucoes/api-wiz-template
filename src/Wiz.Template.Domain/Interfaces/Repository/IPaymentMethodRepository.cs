using System;
using System.Threading.Tasks;
using Wizco.Common.Base;

namespace Wiz.Template.Domain.Interfaces.Repository
{
    public interface IPaymentMethodRepository : IRepository
    {
        Task<bool> ExistsByIdAsync(Guid paymentMethodId);
    }
}
