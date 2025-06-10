using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Wiz.Template.Domain.Entities;
using Wiz.Template.Domain.Interfaces.Repository;
using Wizco.Common.DataAccess.Dapper;
using Wizco.Common.DataAccess.Entity;

namespace Wiz.Template.Infra.Repository;

public class PaymentMethodRepository(SqlServerContext context, DapperContext dapperContext) : Wizco.Common.DataAccess.Repository(context, dapperContext), IPaymentMethodRepository
{
    public async Task<bool> ExistsByIdAsync(string paymentMethodId) =>
        await DbContext.Set<PaymentMethod>()
            .AsNoTracking()
            .AnyAsync(x => x.Id == paymentMethodId);
}
