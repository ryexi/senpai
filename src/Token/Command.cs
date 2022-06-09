using System.Runtime.CompilerServices;
using static Senpai.Exception;

namespace Senpai.Token;

[AttributeUsage(AttributeTargets.Method,
                AllowMultiple = false,
                Inherited = false)]
public sealed class Command : Attribute
{
    /// <summary>
    /// Declare a method as a command.
    /// </summary>
    public Command(string Name,
                   string? Description                     = default,
                   [CallerFilePath]   string? __reserved_1 = default,
                   [CallerMemberName] string? __reserved_2 = default,
                   [CallerLineNumber] int __reserved_3     = default)
    {
        if (string.IsNullOrWhiteSpace(this.Name = Name))
            throw new ArgumentNullException(nameof(Name));

        if (string.IsNullOrWhiteSpace(this.Description = Description ?? string.Empty))
            this.Description = "No description provided.";

        this.SourceProvider = new StackInfo() {
            File    = __reserved_1,
            Member  = __reserved_2,
            Line    = (uint)__reserved_3
        };
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
    /// The description of the command.
    /// </summary>
    public string Description
    {
        get;
        set;
    }

    /// <summary>
    /// The source of the caller.
    /// </summary>
    /// <remarks>Debugging information</remarks>
    internal StackInfo SourceProvider
    {
        get;
        private set;
    }
}