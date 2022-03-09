using Wiz.Template.Domain.Entities;

namespace Wiz.Template.Domain.Interfaces.Repositories
{
    public interface IExampleDapperRepository : IEntityBaseRepository<Example>,
        IDapperReadRepository<Example>
    {
        // Assinatura do seus métodos
    }
}
