using EMSApp.Core.Interfaces;
using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Caching.Memory;

namespace EMSApp.Core.Services
{
    public class BaseService
    {
        protected readonly ILocalizationService L;

        protected readonly ILogger<BaseService> Logger;

        protected readonly ITenantContext TenantContext;

        protected readonly IMemoryCache Cache;

        protected readonly IAppRepository AppRepository;

        protected readonly IHostRepository HostRepository;

        protected readonly IErrorProvider _EP;

        public BaseService(IServiceProvider serviceProvider)
        {
            L = serviceProvider.GetService<ILocalizationService>();
            Logger = serviceProvider.GetService<ILogger<BaseService>>();
            TenantContext = serviceProvider.GetService<ICurrentTenantContextAccessor>()?.TenantContext;
            Cache = serviceProvider.GetService<IMemoryCache>();
            AppRepository = serviceProvider.GetService<IAppRepository>();
            HostRepository = serviceProvider.GetService<IHostRepository>();
            _EP = serviceProvider.GetService<IErrorProvider>();
        }
    }
}
