namespace api.Configs;

public class JwtConfig
{
    public string SecretKey { get; set; } = String.Empty;
    public string Issuer { get; set; }= String.Empty;
    public string Audience { get; set; }= String.Empty;
    public int ExpirationMinutes { get; set; }
}