using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Wiz.Template.Domain.Entities;
using Wiz.Template.Domain.Interfaces.Repository;
using Wizco.Common.Base.Paging;
using Wizco.Common.DataAccess;
using Wizco.Common.DataAccess.Dapper;
using Wizco.Common.DataAccess.Entity;

namespace Wiz.Template.Infra.Repository;

public class ContactsRepository(SqlServerContext context, DapperContext dapperContext) : Wizco.Common.DataAccess.Repository(context, dapperContext), IContactsRepository
{
    public async Task<PagedList<Contacts>> GetAllAsync(int page = 1, int pageSize = 20) => 
        await DbContext.Set<Contacts>()
                .AsNoTracking()
                .AsQueryable()
                .ToPagedListAsync(page, pageSize);
}
