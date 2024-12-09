using System.Collections.Generic;
using System.Threading.Tasks;
using Wiz.Template.Domain.Entities;
using Wizco.Common.Base;
using Wizco.Common.Base.Paging;

namespace Wiz.Template.Domain.Interfaces.Repository
{
    /// <summary>
    /// Contacts Repository
    /// </summary>
    /// <seealso cref="Wizco.Common.Base.IRepository" />
    public interface IContactsRepository : IRepository
    {
        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        Task<PagedList<Contacts>> GetAllAsync(int page = 1, int pageSize = 20);
    }
}
