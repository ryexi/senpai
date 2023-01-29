using Maid.Properties;

namespace Maid;

/// <summary>
/// A symbol defining the parameters that can be passed to a <see cref="Command">command</see>.
/// </summary>
public sealed class CommandProperty : ISymbol
{
    private string? _description;
    private string? _name;
    private string? _synopsis;

    /// <summary>
    /// The description of the symbol, shown in help.
    /// </summary>
    public string? Description
    {
        get => _description ?? Resources.SymbolNoDescriptionProvided;
        set => _description = value;
    }

    public bool IsHidden
    {
        get;
        set;
    }

    /// <summary>
    /// The name of the symbol.
    /// </summary>
    public string? Name
    {
        get => _name;

        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentNullException(nameof(value));
            }

            _name = value;
        }
    }

    /// <summary>
    /// The summary of the symbol, shown in help.
    /// </summary>
    public string? Synopsis
    {
        get => _synopsis;
        set => _synopsis = value;
    }
}