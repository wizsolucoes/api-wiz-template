using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Wiz.Template.Domain.Entities;
using Wiz.Template.Domain.Interfaces.Repository;
using Wizco.Common.DataAccess.Dapper;
using Wizco.Common.DataAccess.Entity;

namespace Wiz.Template.Infra.Repository;

public class MerchantRepository(SqlServerContext context, DapperContext dapperContext) : Wizco.Common.DataAccess.Repository(context, dapperContext), IMerchantRepository
{
    public async Task<bool> ExistsByIdAsync(int merchantId) =>
        await DbContext.Set<Merchant>()
            .AsNoTracking()
            .AnyAsync(x => x.Id == merchantId);
}
