using Microsoft.EntityFrameworkCore.Storage;
using Wiz.Template.Domain.Interfaces.UoW;
using Wiz.Template.Infra.Context;

namespace Wiz.Template.Infra.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        public readonly EntityContext _entityContext;
        private IDbContextTransaction _transaction;

        #pragma warning disable CS8618
        public UnitOfWork(EntityContext entityContext)
        {
            _entityContext = entityContext;
        }

        public int Commit()
        {
            return _entityContext.SaveChanges();
        }

        public void BeginTransaction()
        {
            _transaction = _entityContext.Database.BeginTransaction();
        }

        public void BeginCommit()
        {
            _transaction.Commit();
        }

        public void BeginRollback()
        {
            _transaction.Rollback();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _entityContext.Dispose();

                if (_transaction is not null)
                {
                    _transaction.Dispose();
                }
            }
        }
    }
}
