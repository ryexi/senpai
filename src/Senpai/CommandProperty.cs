namespace Senpai;

/// <summary>
/// A symbol defining the parameters that can be passed to a <see cref="Command">command</see>.
/// </summary>
public sealed class CommandProperty : Symbol
{
    private string? _synopsis;

    /// <summary>
    /// The summary of the symbol, shown in help.
    /// </summary>
    public string? Synopsis
    {
        get => _synopsis;
        set => _synopsis = value;
    }
}