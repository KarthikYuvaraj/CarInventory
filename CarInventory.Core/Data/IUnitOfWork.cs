using CarInventory.Core.Data.Repositories;
using CarInventory.Core.Entities;
using System;
using System.Threading.Tasks;

namespace CarInventory.Core.Data
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity;

        void BeginTransaction();

        int Commit();

        Task<int> CommitAsync();

        void Rollback();

        void Dispose(bool disposing);

    }
}
