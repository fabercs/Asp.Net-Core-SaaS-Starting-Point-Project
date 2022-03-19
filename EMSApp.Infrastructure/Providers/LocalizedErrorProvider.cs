using EMSApp.Core;
using EMSApp.Core.DTO;
using EMSApp.Core.Interfaces;

namespace EMSApp.Infrastructure.Providers
{
    public class LocalizedErrorProvider : IErrorProvider
    {
        private readonly ILocalizationService _L;

        public LocalizedErrorProvider(ILocalizationService localizationService)
        {
            _L = localizationService;
        }
        public Error GetError(string key) => new(key, _L.GetLocalizedValue(Constants.Errors[key]));
        
    }
}
