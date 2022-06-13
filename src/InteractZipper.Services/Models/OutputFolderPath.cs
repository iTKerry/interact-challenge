namespace InteractZipper.Services.Models;

public sealed record OutputFolderPath
{
    private readonly Uri? _value;
    
    public Uri? Value
    {
        get => _value ?? throw new ArgumentNullException(nameof(Value),
            $"You are trying to access invalid {nameof(OutputFolderPath)} state.");
        init => _value = value;
    }

    public OutputFolderPath(Uri? value)
    {
        Value = value;
    }
    
    public bool IsValid => Value?.IsWellFormedOriginalString() ?? false;
}