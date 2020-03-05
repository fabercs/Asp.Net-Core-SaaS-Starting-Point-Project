using EMSApp.Core.Interfaces;

namespace EMSApp.Core.DTO
{
    public class Error
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public Error(){}
        public Error(string errorCode, string errorMessage)
        {
            Code = errorCode;
            Description = errorMessage;
        }

    }
}
