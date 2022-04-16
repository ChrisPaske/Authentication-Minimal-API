namespace Authentication_Minimal_API.Core;

public abstract class ApplicationServiceBase
{
    protected ApplicationServiceBase(IConfiguration configuration)
    {
        Parameter.NullCheck(configuration, nameof(configuration));

        AppSettings = AppSettings.Initialize(configuration);
    }

    protected AppSettings? AppSettings { get; }
}