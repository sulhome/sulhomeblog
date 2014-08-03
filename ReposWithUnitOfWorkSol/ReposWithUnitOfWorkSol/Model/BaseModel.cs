using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReposWithUnitOfWorkSol.Model
{
    public abstract class BaseModel<T>
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public virtual T Id { get; set; }

        [Required, StringLength(maximumLength: 250)]
        public virtual string Name { get; set; }

        [StringLength(maximumLength: 1000)]
        public virtual string Description { get; set; }
    }
}