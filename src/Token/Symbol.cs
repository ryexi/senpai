using static Senpai.Exception;

namespace Senpai.Token;

public abstract class Symbol : Attribute
{
    /// <summary>
    /// The description of the symbol.
    /// </summary>
    public virtual string Description
    {
        get;
        set;
    } = "No description provided.";

    /// <summary>
    /// Defines the arity of an option or argument.
    /// </summary>
    public ArgumentArity Arity
    {
        get;
        set;
    }

    /// <summary>
    /// The name of the symbol.
    /// </summary>
    internal virtual string Name
    {
        get;
        set;
    } = string.Empty;

    /// <summary>
    /// Debugging information about the source of the caller.
    /// </summary>
    internal virtual StackTraceObject StackTrace
    {
        get;
        set;
    }

    /// <summary>
    /// The position of its respective parameter.
    /// </summary>
    internal virtual uint Index
    {
        get;
        set;
    }
}