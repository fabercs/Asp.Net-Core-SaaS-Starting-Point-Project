using EMSApp.Core.Interfaces;
using EMSApp.Webapi.Resources;
using Microsoft.Extensions.Localization;
using System.Reflection;

namespace EMSApp.Webapi.Localization
{
    public class LocalizationService : ILocalizationService
    {
        private readonly IStringLocalizer _localizer;

        public LocalizationService(IStringLocalizerFactory localizerFactory)
        {
            var type = typeof(SharedResource);
            var assemblyName = new AssemblyName(type.GetTypeInfo().Assembly.FullName);
            _localizer = localizerFactory.Create("SharedResource", assemblyName.Name);
        }
        public string GetLocalizedValue(string key)
        {
            return _localizer[key];
        }
    }
}
