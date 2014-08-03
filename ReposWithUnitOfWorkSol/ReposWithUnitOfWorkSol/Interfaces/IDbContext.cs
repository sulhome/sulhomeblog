using System;
using System.Data.Entity;
namespace ReposWithUnitOfWorkSol.Interfaces
{
    public interface IDbContext : IDisposable
    {
        IDbSet<TEntity> Set<TEntity>() where TEntity : class;
        int SaveChanges();
    }
}
