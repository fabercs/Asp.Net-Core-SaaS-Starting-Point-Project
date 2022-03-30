namespace EMSApp.Shared
{
    public class Error
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public Error(string errorCode, string errorMessage)
        {
            Code = errorCode;
            Description = errorMessage;
        }

    }
}
