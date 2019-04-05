using System;

namespace Wiz.Template.Domain.Interfaces.Repository
{
    public interface IEntityBaseRepository<TEntity> : IDisposable where TEntity : class
    {
        void Add(TEntity obj);
        void Update(TEntity obj);
        void Remove(TEntity obj);
    }
}
