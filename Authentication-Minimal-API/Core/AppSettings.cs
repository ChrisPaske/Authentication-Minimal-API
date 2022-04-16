namespace Authentication_Minimal_API.Core;

public class AppSettings
{
    public const string Name = "AppSettings";

    public string? AuthSecurityKey { get; set; }
    public int? AuthExpirationHours { get; set; }
    public string? DbConnectionString { get; set; }

    public static AppSettings Initialize(IConfiguration configuration)
    {
        var appSettings = configuration.GetSection(Name).Get<AppSettings>();

        Parameter.NullCheck(appSettings, nameof(AppSettings));
        Parameter.NullCheck(appSettings.AuthSecurityKey, nameof(AuthSecurityKey));
        Parameter.NullCheck(appSettings.AuthExpirationHours, nameof(AuthExpirationHours));
        Parameter.NullCheck(appSettings.DbConnectionString, nameof(DbConnectionString));

        return appSettings;
    }
}