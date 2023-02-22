using AutoMapper;
using EMSApp.Core.Interfaces;
using EMSApp.Shared;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace EMSApp.Core.Services
{
    public abstract class BaseService
    {
        private readonly ILazyServiceProvider _lazyServiceProvider;

        protected ILocalizationService L => _lazyServiceProvider.LazyGetRequiredService<ILocalizationService>();
        protected ILogger<BaseService> Logger => _lazyServiceProvider.LazyGetRequiredService<ILogger<BaseService>>();
        protected IMapper Mapper => _lazyServiceProvider.LazyGetRequiredService<IMapper>();
        protected IMemoryCache Cache => _lazyServiceProvider.LazyGetRequiredService<IMemoryCache>();
        protected IAppRepository AppRepository =>
            _lazyServiceProvider.LazyGetRequiredService<IAppRepository>();
        protected IHostRepository HostRepository =>
            _lazyServiceProvider.LazyGetRequiredService<IHostRepository>();
        protected IErrorProvider ErrorProvider =>
            _lazyServiceProvider.LazyGetRequiredService<IErrorProvider>();
        public BaseService(ILazyServiceProvider serviceProvider)
        {
            _lazyServiceProvider = serviceProvider;
        }
    }
}
