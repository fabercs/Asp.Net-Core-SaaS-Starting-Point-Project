using EMSApp.Core.DTO;

namespace EMSApp.Core.Interfaces
{
    public interface IErrorProvider
    {
        Error GetError(string key);
    }
}
