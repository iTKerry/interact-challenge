using InteractZipper;
using InteractZipper.Services;
using InteractZipper.Services.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

void ConfigureLogging(ILoggingBuilder builder)
{
    builder
        .ClearProviders()
        .AddConsole();
    
#if DEBUG
    builder.SetMinimumLevel(LogLevel.Debug);
#else
    builder.SetMinimumLevel(LogLevel.Information);
#endif
}

void ConfigureServices(IServiceCollection services, IConfiguration cfg)
{
    services
        .AddLogging()
        .AddHostedService<App>()
        .AddServices()

        .AddTransient(_ => new FilePaths(cfg
            .GetSection("file")
            .GetChildren()
            .Where(str => str.Value is not null && Uri.IsWellFormedUriString(str.Value, UriKind.RelativeOrAbsolute))
            .Select(s => new Uri(s.Value ?? string.Empty, UriKind.RelativeOrAbsolute))
            .ToArray()))
        
        .AddTransient(_ =>
        {
            var value = cfg.GetSection("out").Value;
            return new OutputFolderPath(value is not null
                ? new Uri(value, UriKind.RelativeOrAbsolute)
                : null);
        });
}

Host.CreateDefaultBuilder(args)
    .ConfigureLogging(ConfigureLogging)
    .ConfigureServices((context, collection) => ConfigureServices(collection, context.Configuration))
    .RunConsoleAsync();