using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using ReposWithUnitOfWorkSol.Interfaces;
using ReposWithUnitOfWorkSol.Model;

namespace ReposWithUnitOfWorkSol.Data
{
    public class EFContext : DbContext, IDbContext
    {
        public EFContext()
            : base("ReposWithUnitOfWorkDB")
        {
            Database.SetInitializer<EFContext>(new DBInitializer());
        }

        public new IDbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
            
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Entity<User>();
            modelBuilder.Entity<Role>();
            modelBuilder.Entity<Team>();
            base.OnModelCreating(modelBuilder);
        }
    }
}