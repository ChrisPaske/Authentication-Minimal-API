namespace Authentication_Minimal_API.Core;

public abstract class ApplicationServiceBase
{
    protected ApplicationServiceBase(IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(configuration, nameof(configuration));

        AppSettings = AppSettings.Initialize(configuration);
    }

    protected AppSettings AppSettings { get; }
}