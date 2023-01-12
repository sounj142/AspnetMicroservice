using Ocelot.Configuration.File;

namespace OcelotApiGateway;

public class GlobalHosts : Dictionary<string, Uri>
{ }

public static class FileConfigurationExtensions
{
    public static void ConfigureDownstreamHostAndPortsPlaceholders(
        this WebApplicationBuilder builder)
    {
        builder.Services.PostConfigure<FileConfiguration>(fileConfiguration =>
        {
            var globalHosts = builder.Configuration
                .GetSection($"{nameof(FileConfiguration.GlobalConfiguration)}:Hosts")
                .Get<GlobalHosts>();

            foreach (var route in fileConfiguration.Routes)
            {
                ConfigureRoute(route, globalHosts);
            }
        });
    }

    private static void ConfigureRoute(FileRoute route, GlobalHosts globalHosts)
    {
        foreach (var hostAndPort in route.DownstreamHostAndPorts)
        {
            var host = hostAndPort.Host;
            if (host.StartsWith("{") && host.EndsWith("}"))
            {
                var placeHolder = host.TrimStart('{').TrimEnd('}');
                if (globalHosts.TryGetValue(placeHolder, out var uri))
                {
                    route.DownstreamScheme = uri.Scheme;
                    hostAndPort.Host = uri.Host;
                    hostAndPort.Port = uri.Port;
                }
            }
        }
    }
}