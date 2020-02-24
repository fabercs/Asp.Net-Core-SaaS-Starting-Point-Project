using System;

namespace EMSApp.Infrastructure.Auth
{
    public class JwtSettings
    {
        public string SecretKey { get; set; }
        public TimeSpan TokenLifeTime { get; set; }
    }
}
