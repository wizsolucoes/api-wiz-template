using Wiz.Template.Domain.Entities;
using Wiz.Template.Domain.Interfaces.Repositories;
using Wiz.Template.Infra.Context;

namespace Wiz.Template.Infra.Repositories
{
    public class ExampleRepository : EntityBaseRepository<Example>,
        IExampleRepository
    {
        public ExampleRepository(EntityContext context) : base(context)
        {
            //
        }
    }
}
