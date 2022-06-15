using static Senpai.Exception;

namespace Senpai.Token;

public abstract class Symbol : Attribute
{
    /// <summary>
    /// The name of the argument.
    /// </summary>
    internal virtual string Name
    {
        get;
        set;
    } = string.Empty;

    /// <summary>
    /// The description of the argument.
    /// </summary>
    public virtual string Description
    {
        get;
        set;
    } = "No description provided.";

    /// <summary>
    /// Debugging information about the source of the caller.
    /// </summary>
    internal virtual StackInfo Source
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