using System.Diagnostics;
using InteractZipper.Services.Abstractions;
using InteractZipper.Services.Models;
using Microsoft.Extensions.Logging;

namespace InteractZipper.Services.Zipper;

public sealed class FilesZipperLogger : IFilesZipper
{
    private readonly ILogger<IFilesZipper> _logger;
    private readonly IFilesZipper _filesZipper;

    public FilesZipperLogger(ILogger<FilesZipperLogger> logger, IFilesZipper filesZipper)
    {
        _logger = logger;
        _filesZipper = filesZipper;
    }

    public void Zip(FilePaths paths,
        OutputFolderPath outputFolderPath)
    {
        var sw = new Stopwatch();
        try
        {
            sw.Start();
            _filesZipper.Zip(paths, outputFolderPath);
        }
        finally
        {
            sw.Stop();
            _logger.LogInformation("Zip process elapsed: {Elapsed}", sw.Elapsed.ToString());
        }
    }
}