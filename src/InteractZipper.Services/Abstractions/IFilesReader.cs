using InteractZipper.Services.Models;

namespace InteractZipper.Services.Abstractions;

public interface IFilesReader
{
    IReadOnlyList<Dictionary<string, object>> Read(FilePaths files);
}