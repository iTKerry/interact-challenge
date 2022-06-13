using InteractZipper.Services.Abstractions;
using InteractZipper.Services.Zipper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace InteractZipper.Services;

public static class ServicesModule
{
    public static IServiceCollection AddServices(this IServiceCollection services) => services
        .AddTransient<IFilesReader, FilesReader>()
        .AddTransient<IFilesWriter, FilesWriter>()

        .AddTransient<FilesZipper>()
        .AddTransient<IFilesZipper, FilesZipperLogger>(sp =>
            new FilesZipperLogger(
                sp.GetService<ILogger<FilesZipperLogger>>()!,
                sp.GetService<FilesZipper>()!));
}