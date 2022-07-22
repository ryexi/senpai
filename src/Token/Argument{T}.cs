namespace Senpai.Token;

/// <summary>
/// Represents <see cref="System.CommandLine.Argument"/>
/// </summary>
/// <typeparam name="T">The type of the argument.</typeparam>
[AttributeUsage(AttributeTargets.Method,
                AllowMultiple = true,
                Inherited = false)]
public sealed class Argument<T> : Symbol<T>
{
    /// <summary>
    /// Represents <see cref="System.CommandLine.Argument"/>
    /// </summary>
    /// <param name="Id">The position of its parameter.</param>
    /// <param name="Name">The name of the argument.</param>
    public Argument(uint Id, string Name) : this(Id, Name, string.Empty)
    {
    }

    /// <summary>
    /// Represents <see cref="System.CommandLine.Argument"/>
    /// </summary>
    /// <param name="Id">The position of its parameter.</param>
    /// <param name="Name">The name of the argument.</param>
    /// <param name="Description">The description of the argument.</param>
    public Argument(uint Id, string Name, string Description)
    {
        if (string.IsNullOrWhiteSpace(this.Name = Name))
            throw new ArgumentNullException(nameof(Name));

        if (!string.IsNullOrEmpty(Description))
            this.Description = Description;

        this.Index = Id;
    }

    /// <summary>
    /// The name used in help output to describe the argument.
    /// </summary>
    public string? HelpName
    {
        get;
        set;
    }
}