using InteractZipper.Services.Models;

namespace InteractZipper.Services.Abstractions;

public interface IFilesZipper
{
    void Zip(FilePaths paths, OutputFolderPath outputFolderPath);
}