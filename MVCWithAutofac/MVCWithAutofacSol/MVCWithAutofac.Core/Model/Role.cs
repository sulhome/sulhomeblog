using System.Collections.Generic;

namespace MVCWithAutofac.Core.Model
{
    public class Role : BaseModel<int>
    {
        public virtual IEnumerable<User> Users { get; set; }
    }
}
