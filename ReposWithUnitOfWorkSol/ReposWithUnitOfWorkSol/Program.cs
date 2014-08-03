using System;
using System.Linq;
using ReposWithUnitOfWorkSol.Data;
using ReposWithUnitOfWorkSol.Model;
using Autofac;
using ReposWithUnitOfWorkSol.Interfaces;

namespace ReposWithUnitOfWorkSol
{
    public class Program
    {
        private static IContainer container;
        private static IConsole console;

        public static void SetContainer(IContainer mockedContainer)
        {
            container = mockedContainer;
        }

        public static void Main(string[] args)
        {
            if (container == null)
            {
                container = BootStrap.BuildContainer();
            }

            console = container.Resolve<IConsole>();

            var userChoice = string.Empty;

            while (userChoice != "5")
            {
                console.WriteOutputOnNewLine("\nChoose one of the following options by entering option's number:\n ");
                console.WriteOutputOnNewLine("1- Initialize DB\n");
                console.WriteOutputOnNewLine("2- Run and commit a transaction\n");
                console.WriteOutputOnNewLine("3- Run and rollback a transaction\n");
                console.WriteOutputOnNewLine("4- Select users in a team\n");
                console.WriteOutputOnNewLine("5- Exit");

                userChoice = console.ReadInput();

                switch (userChoice)
                {
                    case "1":
                        InitializeDB();
                        break;
                    case "2":
                        RunAndCommitTransaction();
                        break;
                    case "3":
                        RunAndRollbackTransaction();
                        break;
                    case "4":
                        SelectUsersInTeam();
                        break;
                }
            }
        }

        private static void InitializeDB()
        {
            console.WriteOutputOnNewLine("\nInitializing the db....");
            using (var ctx = new EFContext())
            {
                var dummyUser = ctx.Set<User>().FirstOrDefault();
            }
            console.WriteOutputOnNewLine("\nInitializing db has been completed....");
        }

        private static void SelectUsersInTeam()
        {
            using (var repo = container.Resolve<ITeamRepository>())
            {
                var teams = repo.GetAll<Team>().ToList();
                teams.ForEach(t => console.WriteOutputOnNewLine(string.Format("Team Name:{0}, Team Id: {1}", t.Name, t.Id)));

                console.WriteOutput("Enter team id: ");
                var teamId = console.ReadInput();

                repo.GetUsersInTeam(int.Parse(teamId)).ForEach(
                    u => console.WriteOutputOnNewLine(string.Format("Name: {0}, Email: {1}", u.Name, u.email))
                    );
            }
        }

        private static void RunAndRollbackTransaction()
        {
            using (var uof = container.Resolve<IUnitOfWork>())
            using (var repo = container.Resolve<IRepository>())
            {
                uof.StartTransaction();

                console.WriteOutputOnNewLine("\nStarting a tranaction....");

                var role = new Role { Name = "ProductOwner", Description = "Product Owner role description" };
                repo.Insert<Role>(role);

                var user = new User { Name = "Mark", Description = "Mark user description", email = "Mark@email.com", Password = "123" };
                repo.Insert<User>(user);
                user.Role = role;
                user.Team = repo.GetAll<Team>().FirstOrDefault(t => t.Name.Equals("Los Banditos"));

                console.WriteOutputOnNewLine("\nSaving changes....");
                repo.SaveChanges();

                console.WriteOutputOnNewLine("\nRolling back the transaction....");
                console.WriteOutputOnNewLine(string.Format("\nThe tranaction has been rolled back"));
            }
        }

        private static void RunAndCommitTransaction()
        {
            using (var uof = container.Resolve<IUnitOfWork>())
            using (var repo = container.Resolve<IRepository>())
            {
                uof.StartTransaction();

                console.WriteOutputOnNewLine("\nStarting a tranaction....");

                var role = new Role { Name = "Tester", Description = "Tester description" };
                repo.Insert<Role>(role);

                var user = new User { Name = "Andy", Description = "Andy user description", email = "Andy@email.com", Password = "123" };
                repo.Insert<User>(user);
                user.Role = role;
                user.Team = repo.GetAll<Team>().FirstOrDefault(t => t.Name.Equals("Los Banditos"));

                repo.SaveChanges();
                uof.CommitTransaction();

                console.WriteOutputOnNewLine(string.Format("\nThe tranaction has been commited.\nUser '{0}' and Role '{1}' were added successfully", user.Name, role.Name));
            }
        }
    }
}