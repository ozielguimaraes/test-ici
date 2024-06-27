namespace TesteICI.Infra.CrossCutting.Security.Settings
{
    public class JwtSetting
    {
        public string Secret { get; set; }
        public string ValidIssuer { get; set; }
        public string ValidAudience { get; set; }
        public long ExpirationInSeconds { get; set; }
    }
}
