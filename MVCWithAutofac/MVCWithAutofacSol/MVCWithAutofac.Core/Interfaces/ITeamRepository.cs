using MVCWithAutofac.Core.Model;
using System;
namespace MVCWithAutofac.Core.Interfaces
{
    public interface ITeamRepository : IRepository
    {
        System.Collections.Generic.List<User> GetUsersInTeam(int teamId);
    }
}
