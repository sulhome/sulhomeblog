using System.ComponentModel.DataAnnotations;

namespace ReposWithUnitOfWorkSol.Model
{
    public class User : BaseModel<int>
    {
        public string Password { get; set; }
        public string email { get; set; }

        [Required, StringLength(100)]
        public override string Name { get; set; }

        public int? TeamId { get; set; }
        public virtual Team Team { get; set; }

        public int RoleId { get; set; }
        public virtual Role Role { get; set; }
    }
}
