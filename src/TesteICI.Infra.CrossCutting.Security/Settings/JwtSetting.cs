namespace TesteICI.Infra.CrossCutting.Security.Settings
{
    public class JwtSetting
    {
        public string SecretKey { get; set; } = string.Empty;
        public string Issuer { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
        public long ExpirationInSeconds { get; set; }
    }
}
