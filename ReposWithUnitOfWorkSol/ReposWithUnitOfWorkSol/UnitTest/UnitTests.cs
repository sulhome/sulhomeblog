using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ReposWithUnitOfWorkSol.Interfaces;
using ReposWithUnitOfWorkSol.Model;

namespace ReposWithUnitOfWorkSol.UnitTest
{
    [TestClass]
    public class UnitTests
    {
        [TestMethod]
        public void RunAndCommitTransaction_WithDefault()
        {
            // Arrange
            var contextMock = new Mock<IDbContext>();
            contextMock.Setup(a => a.Set<User>()).Returns(Mock.Of<IDbSet<User>>);
            contextMock.Setup(a => a.Set<Role>()).Returns(Mock.Of<IDbSet<Role>>);
            contextMock.Setup(a => a.Set<Team>()).Returns(Mock.Of<IDbSet<Team>>);

            var unitOfWorkMock = new Mock<IUnitOfWork>();

            var consoleMock = new Mock<IConsole>();
            consoleMock.Setup(c => c.ReadInput()).Returns(new Queue<string>(new[] { "2", "5" }).Dequeue);

            var container = GetMockedContainer(contextMock.Object, unitOfWorkMock.Object, consoleMock.Object);

            // Act
            Program.SetContainer(container);
            Program.Main(null);

            // Assert
            unitOfWorkMock.Verify(a => a.StartTransaction(), Times.Exactly(1));
            unitOfWorkMock.Verify(a => a.CommitTransaction(), Times.Exactly(1));
        }

        private IContainer GetMockedContainer(IDbContext ctx, IUnitOfWork uow, IConsole console)
        {
            var builder = new ContainerBuilder();

            builder.RegisterInstance(ctx).As<IDbContext>();
            builder.RegisterInstance(uow).As<IUnitOfWork>();
            builder.RegisterInstance(new Mock<IRepository>().Object).As<IRepository>();
            builder.RegisterInstance(console).As<IConsole>();

            return builder.Build();
        }
    }
}