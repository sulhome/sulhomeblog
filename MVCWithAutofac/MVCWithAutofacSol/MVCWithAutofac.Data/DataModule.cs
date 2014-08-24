using Autofac;
using MVCWithAutofac.Core.Interfaces;

namespace MVCWithAutofac.Data
{
    public class DataModule : Module
    {
        private string connStr;
        public DataModule(string connString)
        {
            this.connStr = connString;
        }
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => new EFContext(this.connStr)).As<IDbContext>().InstancePerRequest();
            builder.RegisterType<SqlRepository>().As<IRepository>().InstancePerRequest();
            builder.RegisterType<TeamRepository>().As<ITeamRepository>().InstancePerRequest();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerRequest();         

            base.Load(builder);
        }
    }
}
