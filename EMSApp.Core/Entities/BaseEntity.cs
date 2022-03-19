using EMSApp.Core.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EMSApp.Core.Entities
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

        [Timestamp]
        public byte[] Version { get; set; }
        object IEntity.Id { get => this.Id; set { } }
    }

    public abstract class BaseEntity : BaseEntity<Guid>{}
}
