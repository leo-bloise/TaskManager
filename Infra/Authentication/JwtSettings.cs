namespace TaskManager.Infra.Authentication;

public class JwtSettings
{
    public string Secret { get; set; }
    public int ExpiresAtMinutes { get; set; }
    public string Issuer { get; set; }
    public string Audience { get; set; }
}