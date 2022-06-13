using System.Text;
using InteractZipper.Services.Abstractions;
using InteractZipper.Services.Models;

namespace InteractZipper.Services.Zipper;

public sealed class FilesZipper : IFilesZipper
{
    private readonly IFilesReader _reader;
    private readonly IFilesWriter _writer;
    

    public FilesZipper(
        IFilesReader reader, 
        IFilesWriter writer)
    {
        _reader = reader;
        _writer = writer;
    }

    public void Zip(FilePaths paths,
        OutputFolderPath outputFolderPath)
    {
        ValidateInputParams(paths, outputFolderPath);
        
        var data = _reader.Read(paths);
        
        _writer.Write(outputFolderPath, data);
    }
    
    private static void ValidateInputParams(FilePaths paths, OutputFolderPath outputFolderPath)
    {
        if (!outputFolderPath.IsValid)
        {
            throw new ArgumentException("Output folder was not specified", nameof(outputFolderPath));
        }

        if (!paths.Value.Any())
        {
            throw new ArgumentException("No one file was not specified", nameof(paths));
        }

        if (!paths.IsValid)
        {
            var invalidPaths = paths.Value
                .Where(uri => !uri.IsWellFormedOriginalString())
                .Select(uri => uri.ToString())
                .Aggregate(new StringBuilder(),
                    (builder, str) => builder.Length == 0
                        ? builder.Append(str)
                        : builder.Append(", ").Append(str),
                    builder => builder.ToString());

            throw new ArgumentException($"Invalid file paths: {invalidPaths}", nameof(paths));
        }
    }
}