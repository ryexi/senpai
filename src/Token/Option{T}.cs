namespace Senpai.Token;

/// <summary>
/// Represents <see cref="System.CommandLine.Option"/>
/// </summary>
/// <typeparam name="T">The type of the option.</typeparam>
[AttributeUsage(AttributeTargets.Method,
                AllowMultiple = true,
                Inherited = false)]
public sealed class Option<T> : Symbol<T>
{
    /// <summary>
    /// Represents <see cref="System.CommandLine.Option"/>
    /// </summary>
    /// <param name="Id">The position of its parameter.</param>
    /// <param name="Name">The name of the option.</param>
    public Option(uint Id, string Name) : this(Id, Name, string.Empty)
    {
    }

    /// <summary>
    /// Represents <see cref="System.CommandLine.Option"/>
    /// </summary>
    /// <param name="Id">The position of its parameter.</param>
    /// <param name="Name">The name of the option.</param>
    /// <param name="Description">The description of the option. </param>
    public Option(uint Id, string Name, string Description)
    {
        if (string.IsNullOrWhiteSpace(this.Name = Name))
            throw new ArgumentNullException(nameof(Name));

        if (!string.IsNullOrEmpty(Description))
            this.Description = Description;

        this.Index = Id;
        this.Aliases = new string[0];
    }

    /// <summary>
    /// A string that can be used on the command line to specify the option.
    /// </summary>
    /// <value></value>
    public string? Alias
    {
        get;
        set;
    }

    /// <summary>
    /// The set of strings that can be used on the command line to specify the option.
    /// </summary>
    public string[] Aliases
    {
        get;
        set;
    }

    /// <summary>
    /// Defines the arity of an option or argument.
    /// </summary>
    public ArgumentArity Arity
    {
        get;
        set;
    }

    /// <summary>
    /// Indicates whether the option is required when its parent command is invoked.
    /// </summary>
    /// <remarks>When an option is required and its parent command is invoked without it, an error results</remarks>
    public bool IsRequired
    {
        get;
        set;
    }
}