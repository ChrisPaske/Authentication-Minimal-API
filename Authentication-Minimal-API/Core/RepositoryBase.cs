namespace Authentication_Minimal_API.Core;

public abstract class RepositoryBase
{
    protected RepositoryBase(IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(configuration, nameof(configuration));

        AppSettings = AppSettings.Initialize(configuration);
    }

    protected AppSettings AppSettings { get; }
}