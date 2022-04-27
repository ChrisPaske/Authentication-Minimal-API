using NBomber.Contracts;
using NBomber.CSharp;
using NBomber.Plugins.Network.Ping;

namespace Authentication_Minimal_API_Test;

public static class NBomberExtensionMethods
{
    public static NBomberContext AddReportingPlugins(this NBomberContext context)
    {
        var pingPluginConfig = PingPluginConfig.CreateDefault(new[] {"localhost"});
        var pingPlugin = new PingPlugin(pingPluginConfig);
        return context.WithWorkerPlugins(pingPlugin);
    }

}