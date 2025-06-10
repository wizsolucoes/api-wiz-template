using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wiz.Template.Domain.Entities;
using Wiz.Template.Domain.Interfaces.Repository;
using Wizco.Common.DataAccess.Dapper;
using Wizco.Common.DataAccess.Entity;

namespace Wiz.Template.Infra.Repository;

public class TransactionRepository(SqlServerContext context, DapperContext dapperContext) : Wizco.Common.DataAccess.Repository(context, dapperContext), ITransactionRepository
{

    /// <summary>
    /// Gets the payments by merchant asynchronous.
    /// </summary>
    /// <param name="input">The input.</param>
    public async Task<List<Transaction>> GetPaymentsByMerchantAsync(int merchantId) =>
        await DbContext.Set<Transaction>()
            .Where(x => x.MerchantId == merchantId)
            .Include(x => x.Merchant)
            .Include(x => x.PaymentMethod)
            .ToListAsync();
}
