using System;

namespace EMSApp.Core.Entities
{

       
    public class Tenant
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Host { get; set; }
        public string ConnectionString { get; set; }

    }
}
