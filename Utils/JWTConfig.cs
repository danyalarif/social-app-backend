using System;

namespace social_app_backend.Utils;

public class JWTConfig
{
    public required string SecretKey { get; set; }
    public required string Issuer { get; set; }
    public required string Audience { get; set; }
    public required int TokenExpiryInMinutes { get; set; }
}
