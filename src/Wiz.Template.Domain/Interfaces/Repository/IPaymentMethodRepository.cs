using System.Threading.Tasks;
using Wizco.Common.Base;

namespace Wiz.Template.Domain.Interfaces.Repository;

public interface IPaymentMethodRepository : IRepository
{
    /// <summary>
    /// Existses the by identifier asynchronous.
    /// </summary>
    /// <param name="paymentMethodId">The payment method identifier.</param>
    /// <returns></returns>
    Task<bool> ExistsByIdAsync(string paymentMethodId);
}
