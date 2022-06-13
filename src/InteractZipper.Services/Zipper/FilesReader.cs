using System.Collections.Concurrent;
using InteractZipper.Services.Abstractions;
using InteractZipper.Services.Models;
using Microsoft.Extensions.Logging;

namespace InteractZipper.Services.Zipper;

public sealed class FilesReader : IFilesReader
{
    private readonly ILogger<FilesReader> _logger;
    private readonly ConcurrentDictionary<string, List<(string header, object item)>> _dictionary = new ();

    public FilesReader(ILogger<FilesReader> logger)
    {
        _logger = logger;
    }

    public IReadOnlyList<Dictionary<string, object>> Read(FilePaths files)
    {
        files.Value
            .AsParallel()
            .ForAll(ReadFile);
        
        return _dictionary
            .Select(x => x.Value
                .OrderBy(t => t.header)
                .ToDictionary(t => t.header, t => t.item))
            .ToList()
            .AsReadOnly();
    }

    private void AttachRow(string key, Dictionary<string, object> items)
    {
        foreach (var (header, item) in items)
        {
            _dictionary.AddOrUpdate(key,
                _ => new List<(string, object)> { (header, item) },
                (_, list) => list
                    .Where(x => x.header != header)
                    .Append((header, item))
                    .ToList());
        }
    }

    private void ReadFile(Uri filePath)
    {
        var path = filePath.IsAbsoluteUri
            ? filePath.ToString()
            : AppDomain.CurrentDomain.BaseDirectory + filePath;
        
        using var fs = File.OpenRead(path);
        using var sr = new StreamReader(fs);

        var headers = ReadHeader(sr.ReadLine());
        if (headers is null || !headers.Any())
            throw new ArgumentException("File is empty", nameof(headers));

        while (!sr.EndOfStream)
        {
            var (key, items) = ReadRow(sr.ReadLine(), headers);
            AttachRow(key, items);
        }
    }

    private static string[]? ReadHeader(string? line)
    {
        return line?.Split(',').ToArray();
    }
    
    private (string key, Dictionary<string, object> items) ReadRow(string? line, string[] headers)
    {
        if (line is null)
            throw new ArgumentNullException(nameof(line));
        
        try
        {
            var items = line.Split(',').ToArray();
            var key = items[0];

            var result = new Dictionary<string, object>();
            for (var i = 0; i < headers.Length; i++)
            {
                result.Add(headers[i], items[i]);
            }

            return (key, result);
        }
        catch (Exception exn)
        {
            _logger.LogDebug(exn, "Invalid line error: {Line}", line);
            throw new ArgumentException($"Invalid line: {line}", nameof(line));
        }
    }
}