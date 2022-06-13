using InteractZipper.Services.Models;

namespace InteractZipper.Services.Abstractions;

public interface IFilesWriter
{
    void Write(OutputFolderPath outputFolder, IReadOnlyList<Dictionary<string, object>> data);
}