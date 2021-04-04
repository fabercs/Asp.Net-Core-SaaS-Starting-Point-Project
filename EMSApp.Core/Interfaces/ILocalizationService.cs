namespace EMSApp.Core.Interfaces
{
    public interface ILocalizationService
    {
        string GetLocalizedValue(string key);
        string this[string key, params object[] arguments] { get; }
    }
}
