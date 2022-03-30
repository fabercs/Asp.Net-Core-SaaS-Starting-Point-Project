using EMSApp.Core.DTO;
using EMSApp.Shared;

namespace EMSApp.Core.Interfaces
{
    /// <summary>
    /// Used to get error description and code from any key-value source
    /// </summary>
    public interface IErrorProvider
    {
        Error GetError(string key);
        string GetErrorMessage(string key);
    }
}
