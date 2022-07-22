using System.Runtime.CompilerServices;
using static Senpai.Exception;

namespace Senpai.Token;

[AttributeUsage(AttributeTargets.Class,
                AllowMultiple = false,
                Inherited = false)]
public sealed class Command : Attribute
{
    /// <summary>
    /// Declare a class as a command.
    /// </summary>
    /// <param name="Name">The name of the command.</param>
    public Command(string Name,
                   [CallerFilePath] string? _r1 = default,
                   [CallerMemberName] string? _r2 = default,
                   [CallerLineNumber] int _r3 = default)
    {
        if (string.IsNullOrWhiteSpace(this.Name = Name))
            throw new ArgumentNullException(nameof(Name));

        if (!string.IsNullOrEmpty(Summary))
            this.Summary = Summary;

        if (!string.IsNullOrEmpty(Description))
            this.Description = Description;

        this.StackTrace = new StackTraceObject()
        {
            File    = _r1,
            Member  = _r2,
            Line    = (uint)_r3
        };
    }

    /// <summary>
    /// A brief description of the command.
    /// </summary>
    public string Summary
    {
        get;
        set;
    } = string.Empty;

    /// <summary>
    /// The full description of the command.
    /// </summary>
    public string Description
    {
        get;
        set;
    } = "No description provided.";

    /// <summary>
    /// Debugging information about the source of the caller.
    /// </summary>
    internal StackTraceObject StackTrace
    {
        get;
        set;
    }

    internal string Name
    {
        get;
        set;
    } = string.Empty;

}