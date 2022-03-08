namespace Wiz.Template.Domain.Interfaces.Repositories
{
    public interface IEntityBaseRepository<in TEntity> : IDisposable
        where TEntity : class
    {
        void Add(TEntity obj);
        void Update(TEntity obj);
        void Remove(TEntity obj);
    }
}
