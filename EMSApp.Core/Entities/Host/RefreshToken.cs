using EMSApp.Shared;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace EMSApp.Core.Entities
{
    [Table("RefreshToken")]
    public class RefreshToken : BaseEntity
    {
        public string Token { get; set; }
        public DateTime ExpiresOn { get; set; }
        public bool Used { get; set; }
        public bool Invalidated { get; set; }
        public string RemoteIpAddress { get; set; }

        public string JwtId { get; set; }
        public Guid ApplicationUserId { get; set; }

        [ForeignKey(nameof(ApplicationUserId))]
        public ApplicationUser ApplicationUser { get; set; }


    }
}
