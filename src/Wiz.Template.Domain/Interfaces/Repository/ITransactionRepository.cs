using System.Threading.Tasks;
using System;
using Wizco.Common.Base;
using System.Collections.Generic;
using Wiz.Template.Domain.Entities;

namespace Wiz.Template.Domain.Interfaces.Repository;

public interface ITransactionRepository : IRepository
{
    /// <summary>
    /// Gets the payments by merchant asynchronous.
    /// </summary>
    /// <param name="merchantId">The merchant identifier.</param>
    /// <returns></returns>
    Task<List<Transaction>> GetPaymentsByMerchantAsync(int merchantId);
}