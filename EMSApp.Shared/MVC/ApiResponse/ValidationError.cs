namespace EMSApp.Shared
{
    public class ValidationError
    {
        public string Identifier { get; set; }
        public string ErrorMessage { get; set; }
        public ValidationSeverity Severity { get; set; } = ValidationSeverity.Error;
    }
    public enum ValidationSeverity
    {
        Error = 0,
        Warning = 1,
        Info = 2
    }
}
