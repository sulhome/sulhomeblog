using MVCWithAutofac.Core.Model;
using System.Data.Entity;


namespace MVCWithAutofac.Data
{
    public class DBInitializer : CreateDatabaseIfNotExists<EFContext>
    {
        protected override void Seed(EFContext context)
        {
            var sqlRep = new SqlRepository(context);
            var devRole = new Role { Name = "Developer", Description = "Developer Role" };
            var teamLeadRole = new Role { Name = "TeamLead", Description = "Team Leader Role" };
            var managerRole = new Role { Name = "Manager", Description = "Manager Role" };
            sqlRep.Insert<Role>(devRole);
            sqlRep.Insert<Role>(teamLeadRole);
            sqlRep.Insert<Role>(managerRole);

            var team = new Team { Name = "Los Banditos", Description = "Los Banditos teams description" };
            sqlRep.Insert<Team>(team);

            var userSul = new User { Name = "Sul", Description = "Sul user description", email = "sul@email.com", Password = "123", Role = devRole, Team = team };
            var userJames = new User { Name = "James", Description = "James user description", email = "james@email.com", Password = "123", Role = devRole, Team = team };
            sqlRep.Insert<User>(userSul);
            sqlRep.Insert<User>(userJames);

            context.SaveChanges();

        }
    }
}
