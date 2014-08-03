using System.Collections.Generic;

namespace ReposWithUnitOfWorkSol.Model
{
    public class Role : BaseModel<int>
    {
        public virtual IEnumerable<User> Users { get; set; }
    }
}
