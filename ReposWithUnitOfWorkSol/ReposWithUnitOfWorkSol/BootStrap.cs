using Autofac;
using ReposWithUnitOfWorkSol.Data;
using ReposWithUnitOfWorkSol.Interfaces;

namespace ReposWithUnitOfWorkSol
{
    public class BootStrap
    {
        public static IContainer BuildContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<EFContext>().As<IDbContext>();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();
            builder.RegisterType<SqlRepository>().As<IRepository>();
            builder.RegisterType<TeamRepository>().As<ITeamRepository>();
            builder.RegisterType<ConsoleReadWrite>().As<IConsole>();

            return builder.Build();
        }
    }
}