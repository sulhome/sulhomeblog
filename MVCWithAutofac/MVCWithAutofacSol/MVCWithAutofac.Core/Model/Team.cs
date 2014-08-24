using System.Collections.Generic;

namespace MVCWithAutofac.Core.Model
{
    public class Team : BaseModel<int>
    {
        public virtual IEnumerable<User> Users { get; set; }
    }
}
