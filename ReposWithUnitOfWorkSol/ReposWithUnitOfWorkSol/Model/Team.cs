using System.Collections.Generic;

namespace ReposWithUnitOfWorkSol.Model
{
    public class Team : BaseModel<int>
    {
        public virtual IEnumerable<User> Users { get; set; }
    }
}
