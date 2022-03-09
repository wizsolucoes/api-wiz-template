using Microsoft.EntityFrameworkCore;
using Wiz.Template.Domain.Interfaces.Repositories;
using Wiz.Template.Infra.Context;

namespace Wiz.Template.Infra.Repositories
{
    public class EntityBaseRepository<TEntity> :
        IEntityBaseRepository<TEntity> where TEntity : class
    {
        protected readonly EntityContext Db;
        protected readonly DbSet<TEntity> DbSet;

        public EntityBaseRepository(EntityContext context)
        {
            Db = context;
            DbSet = Db.Set<TEntity>();
        }

        public virtual void Add(TEntity obj)
        {
            DbSet.Add(obj);
        }

        public virtual void Update(TEntity obj)
        {
            DbSet.Update(obj);
        }

        public virtual void Remove(TEntity obj)
        {
            DbSet.Remove(obj);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            // Cleanup
            if (disposing)
            {
                Db.Dispose();
            }
        }
    }
}
