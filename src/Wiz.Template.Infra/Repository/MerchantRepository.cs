using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Wiz.Template.Domain.Interfaces.Repository;
using Wiz.Template.Domain.Models;
using Wizco.Common.DataAccess.Dapper;
using Wizco.Common.DataAccess.Entity;

namespace Wiz.Template.Infra.Repository
{
    public class MerchantRepository : Wizco.Common.DataAccess.Repository, IMerchantRepository
    {
        public MerchantRepository(SqlServerContext context, DapperContext dapperContext) : base(context, dapperContext)
        {
        }

        public async Task<bool> ExistsByIdAsync(int merchantId) =>
            await DbContext.Set<Merchant>()
                .AsNoTracking()
                .AnyAsync(x => x.Id == merchantId);
    }
}
