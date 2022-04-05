namespace Senpai.Core;

internal sealed class Senption : Exception
{
    public Senption(string? message, bool isExcept = false, int code = 0)
    {
        Output.Error(isExcept ? 
            $"Exception thrown: {message ?? this.Message}" : message ?? this.Message
        );
        
        Environment.Exit(
            code
        );
    }
}