﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Wiz.Template.Domain.Entities;
using Wiz.Template.Domain.Interfaces.Repository;
using Wizco.Common.DataAccess.Dapper;
using Wizco.Common.DataAccess.Entity;

namespace Wiz.Template.Infra.Repository
{
    public class PaymentMethodRepository : Wizco.Common.DataAccess.Repository, IPaymentMethodRepository
    {
        public PaymentMethodRepository(SqlServerContext context, DapperContext dapperContext) : base(context, dapperContext)
        {
        }

        public async Task<bool> ExistsByIdAsync(string paymentMethodId) =>
            await DbContext.Set<PaymentMethod>()
                .AsNoTracking()
                .AnyAsync(x => x.Id == paymentMethodId);
    }
}
