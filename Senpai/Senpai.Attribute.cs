namespace Senpai;

/// <summary>
/// When set on a method, it identifies the method as a command.
/// </summary>
[AttributeUsage(AttributeTargets.Method,
                AllowMultiple = false,
                Inherited = false)]
public sealed class Command : Attribute
{
    public Command(string name)
    {
        if (string.IsNullOrWhiteSpace(this.Name = name))
            throw new ArgumentNullException();
    }

    /// <summary>
    /// The name of the command.
    /// </summary>
    public string Name
    {
        get;
        private set;
    }

    /// <summary>
    /// If set, the usage output is overriden with this string.
    /// </summary>
    public string? Usage
    {
        get;
        set;
    }

    /// <summary>
    ///  If set to true, the command will not be shown in the help list.
    /// </summary>
    public Boolean Ignore
    {
        get;
        set;
    } = false;

    /// <summary>
    /// The description for the command.
    /// </summary>
    public string? Description
    {
        get;
        set;
    }
}