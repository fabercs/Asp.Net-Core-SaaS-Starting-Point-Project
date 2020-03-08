using EMSApp.Core.Interfaces;
using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace EMSApp.Core.Services
{
    public class BaseService
    {
        protected readonly ILocalizationService L;
        protected readonly ILogger<BaseService> Logger;
        public ITenantContext TenantContext { get; }

        public BaseService(IServiceProvider serviceProvider)
        {
            L = serviceProvider.GetService<ILocalizationService>();
            Logger = serviceProvider.GetService<ILogger<BaseService>>();
            TenantContext = serviceProvider.GetService<ICurrentTenantContextAccessor>()?.CurrentTenant;
        }
    }
}
