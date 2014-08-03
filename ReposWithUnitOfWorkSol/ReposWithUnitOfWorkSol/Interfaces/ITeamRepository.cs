using System;
namespace ReposWithUnitOfWorkSol.Interfaces
{
    public interface ITeamRepository : IRepository
    {
        System.Collections.Generic.List<ReposWithUnitOfWorkSol.Model.User> GetUsersInTeam(int teamId);
    }
}
