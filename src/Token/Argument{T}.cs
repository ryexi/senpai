namespace Senpai.Token;

/// <summary>
/// Represents <see cref="System.CommandLine.Argument"/>
/// </summary>
/// <typeparam name="T">The type of the argument.</typeparam>
[AttributeUsage(AttributeTargets.Method,
                AllowMultiple = true,
                Inherited = false)]
public sealed class Argument<T> : Attribute
{
    /// <summary>
    /// Represents <see cref="System.CommandLine.Argument"/>
    /// </summary>
    /// <param name="Name">The name of the argument.</param>
    public Argument(string Name) : this(null, Name, null)
    {
        // Empty
    }
    
    /// <summary>
    /// Represents <see cref="System.CommandLine.Argument"/>
    /// </summary>
    /// <param name="Index">The order of its parameter.</param>
    /// <param name="Name">The name of the argument</param>
    /// <returns></returns>
    public Argument(uint Index, string Name) : this(Index, Name, null)
    {
        // Empty
    }

    Argument(uint? index, string name, string? description)
    {
        if (string.IsNullOrWhiteSpace(this.Name = name))
            throw new ArgumentException("Contains whitespace or null.");

        this.Description = description ?? "No description provided.";
        this.Index = index;
    }

    public string Name
    {
        get;
        private set;
    }

    /// <summary>
    /// The description of the argument.
    /// </summary>
    public string Description
    {
        get;
        set;
    }

    /// <summary>
    /// Control how many values should be provided.
    /// </summary>
    /// <value></value>
    public ArgumentArity Arity
    {
        get;
        set;
    } = ArgumentArity.Default;

    internal Type GetGenericClassType()
    {
        return typeof(T);
    }

    internal uint? Index
    {
        get;
        private set;
    }
}