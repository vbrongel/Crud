namespace Crud.API.Helpers
{
    public class ApiSettings
    {
        public string Secret { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }

        public ApiSettings(string secret, string clientId, string clientSecret)
        {
            ClientId = clientId;
            ClientSecret = clientSecret;
            Secret = secret;
        }
    }
}
