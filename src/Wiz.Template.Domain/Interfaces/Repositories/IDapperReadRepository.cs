namespace Wiz.Template.Domain.Interfaces.Repositories
{
    public interface IDapperReadRepository<TEntity> where TEntity : class
    {
        Task<TEntity> GetByIdAsync(int id);
    }
}
