using InteractZipper.Services.Abstractions;
using InteractZipper.Services.Models;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace InteractZipper;

public sealed class App : IHostedService
{
    private readonly ILogger<App> _logger;
    private readonly IHostApplicationLifetime _application;
    
    private readonly FilePaths _files;
    private readonly OutputFolderPath _outputFolder;

    private readonly IFilesZipper _zipper;

    public App(
        IHostApplicationLifetime application, ILogger<App> logger, 
        FilePaths files, OutputFolderPath outputFolder, 
        IFilesZipper zipper)
    {
        _logger = logger;
        _files = files;
        _outputFolder = outputFolder;
        _zipper = zipper;
        _application = application;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogDebug("Starting up");
        
        try
        {
            _zipper.Zip(_files, _outputFolder);
        }
        catch (ArgumentException exn)
        {
            _logger.LogError(exn, "Invalid argument received");
        }
        catch (Exception enx)
        {
            _logger.LogCritical(enx, "Fatal error occurred");
        }
        finally
        {
            _application.StopApplication();
        }
        
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogDebug("Shutting down");
        return Task.CompletedTask;
    }
}