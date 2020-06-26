using System;
using System.Collections.Generic;

namespace EMSApp.Core.DTO
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public TenantDto Tenant { get; set; }
        public List<PageDto> PermittedPages { get; set; }
    }
}
