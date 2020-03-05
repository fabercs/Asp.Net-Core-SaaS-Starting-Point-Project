using EMSApp.Core.Interfaces;
using System;
using Microsoft.Extensions.DependencyInjection;

namespace EMSApp.Core.Services
{
    public class BaseService
    {
        protected readonly ILocalizationService L;
        public BaseService(IServiceProvider serviceProvider)
        {
            L = serviceProvider.GetService<ILocalizationService>();
        }
    }
}
