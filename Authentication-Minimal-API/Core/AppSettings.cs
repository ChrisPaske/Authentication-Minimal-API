namespace Authentication_Minimal_API.Core;

public class AppSettings
{
    public const string Name = "AppSettings";

    public string AuthSecurityKey { get; set; }
    public int AuthExpirationHours { get; set; }
    public string DbConnectionString { get; set; }

    public static AppSettings Initialize(IConfiguration configuration)
    {
        var appSettings = configuration.GetSection(Name).Get<AppSettings>();

        ArgumentNullException.ThrowIfNull(appSettings, nameof(AppSettings));
        ArgumentNullException.ThrowIfNull(appSettings.AuthSecurityKey, nameof(AuthSecurityKey));
        ArgumentNullException.ThrowIfNull(appSettings.AuthExpirationHours, nameof(AuthExpirationHours));
        ArgumentNullException.ThrowIfNull(appSettings.DbConnectionString, nameof(DbConnectionString));

        return appSettings;
    }
}