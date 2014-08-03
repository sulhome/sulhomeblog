using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ReposWithUnitOfWorkSol.Interfaces;
using ReposWithUnitOfWorkSol.Model;

namespace ReposWithUnitOfWorkSol.Data
{
    public class TeamRepository : SqlRepository, ITeamRepository
    {
        public TeamRepository(IDbContext context)
            : base(context)
        {

        }
        public List<User> GetUsersInTeam(int teamId)
        {
            return (from u in this.GetAll<User>()
                    where u.TeamId == teamId
                    select u).ToList();
        }
    }
}
