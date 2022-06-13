using System.Text;
using InteractZipper.Services.Abstractions;
using InteractZipper.Services.Models;

namespace InteractZipper.Services.Zipper;

public sealed class FilesWriter : IFilesWriter
{
    public void Write(OutputFolderPath outputFolder, IReadOnlyList<Dictionary<string, object>> data)
    {
        if (!data.Any())
            throw new InvalidOperationException();

        var path = outputFolder.Value!.IsAbsoluteUri
            ? outputFolder.Value.ToString()
            : AppDomain.CurrentDomain.BaseDirectory + outputFolder.Value;

        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);
        
        const string outputFileName = "zipped_result.csv";
        
        using var sw = new StreamWriter($"{path}{Path.DirectorySeparatorChar}{outputFileName}");
        var header = ReduceToLine(data.FirstOrDefault()?.Keys!);
        sw.WriteLine(header);

        foreach (var dict in data)
        {
            var values = dict.Select(kv => kv.Value?.ToString() ?? string.Empty);
            var line = ReduceToLine(values);
            sw.WriteLine(line);
        }
            
        sw.Flush();
        sw.Close(); 
    }
    
    private static string ReduceToLine(IEnumerable<string> array) =>
        array.Aggregate(
            new StringBuilder(),
            (builder, str) => builder.Length <= 0
                ? builder.Append(str)
                : builder.Append(',').Append(str),
            builder => builder.ToString());
}