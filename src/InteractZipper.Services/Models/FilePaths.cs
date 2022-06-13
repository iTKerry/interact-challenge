namespace InteractZipper.Services.Models;

public sealed record FilePaths(Uri[] Value)
{
    public bool IsValid => 
        Value.Any() && 
        Value.All(val => val.IsWellFormedOriginalString());
};