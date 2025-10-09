namespace Backend.Configs;

public class Variable
{
    public static EnviromentVariables Enviroments { get; set; } = new EnviromentVariables();
    public static ConstantVariables Constants { get; set; } = new ConstantVariables();
}

public class EnviromentVariables
{
    // Server ports
    public string PORT_HTTP { get; } = Environment.GetEnvironmentVariable("PORT_HTTP") ?? "4040";
    public string PORT_HTTPS { get; } = Environment.GetEnvironmentVariable("PORT_HTTPS") ?? "7106";
    public string PORT_HTTP_SECONDARY { get; } = Environment.GetEnvironmentVariable("PORT_HTTP_SECONDARY") ?? "5001";

    // Client URL
    public string CLIENT_URL { get; } = Environment.GetEnvironmentVariable("CLIENT_URL") ?? "http://localhost:3000";

    // JWT settings
    public string JWT_SECRET { get; } = Environment.GetEnvironmentVariable("JWT_SECRET") ?? "";
    public string JWT_ISSUER { get; } = Environment.GetEnvironmentVariable("JWT_ISSUER") ?? "";
    public string JWT_AUDIENCE { get; } = Environment.GetEnvironmentVariable("JWT_AUDIENCE") ?? "";
    public string JWT_ACCESS_TOKEN_EXPIRY_MINUTES { get; } = Environment.GetEnvironmentVariable("JWT_ACCESS_TOKEN_EXPIRY_MINUTES") ?? "30";
    public string JWT_REFRESH_TOKEN_EXPIRY_DAYS { get; } = Environment.GetEnvironmentVariable("JWT_REFRESH_TOKEN_EXPIRY_DAYS") ?? "7";

    // MySQL
    public string MYSQL_URI { get; } = Environment.GetEnvironmentVariable("MYSQL_URI") ?? "";

    // Redis
    public string REDIS_URL { get; } = Environment.GetEnvironmentVariable("REDIS_URL") ?? "";

    // Google OAuth
    public string GOOGLE_CLIENT_ID { get; } = Environment.GetEnvironmentVariable("GOOGLE_CLIENT_ID") ?? "";
    public string GOOGLE_CLIENT_SECRET { get; } = Environment.GetEnvironmentVariable("GOOGLE_CLIENT_SECRET") ?? "";

    // Mail
    public string SMTP_HOST { get; } = Environment.GetEnvironmentVariable("SMTP_HOST") ?? "";
    public string SMTP_PORT { get; } = Environment.GetEnvironmentVariable("SMTP_PORT") ?? "";
    public string SMTP_USER { get; } = Environment.GetEnvironmentVariable("SMTP_USER") ?? "";
    public string SMTP_PASS { get; } = Environment.GetEnvironmentVariable("SMTP_PASS") ?? "";
    public string SMTP_FROM { get; } = Environment.GetEnvironmentVariable("SMTP_FROM") ?? "";
}

public class ConstantVariables
{
    public string MyAllowSpecificOrigins { get; } = "_myAllowSpecificOrigins";
}
