using EMSApp.Core.DTO;

namespace EMSApp.Core.Interfaces
{
    /// <summary>
    /// Used to get error description and code from any key-value source
    /// </summary>
    public interface IErrorProvider
    {
        Error GetError(string key);
    }
}
