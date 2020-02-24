namespace EMSApp.Core.DTO.Responses
{

    public class AuthResponse
    {
        public AccessToken AccessToken { get; set; }
        public string RefreshToken { get; set; }

    }
}
