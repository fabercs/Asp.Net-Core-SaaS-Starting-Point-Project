using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EMSApp.Shared
{
    public abstract class BaseEntity<T> : IEntity<T>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public T Id { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.Now;

        public Guid? CreatedBy { get; set; }
        public DateTime? ModifedOn { get; set; }
        public Guid? ModifiedBy { get; set; }
        object IEntity.Id { get => Id; set { } }
    }

    public abstract class BaseEntity : BaseEntity<Guid>{}
}
