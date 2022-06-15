using System.Runtime.CompilerServices;
using static Senpai.Exception;

namespace Senpai.Token;

[AttributeUsage(AttributeTargets.Method,
                AllowMultiple = false,
                Inherited = false)]
public sealed class Command : Symbol
{
    /// <summary>
    /// Declare a method as a command.
    /// </summary>
    /// <param name="Name">The name of the command.</param>
    /// <param name="Description">The description of the command.</param>
    public Command(string Name,
                   string? Description                     = default,
                   [CallerFilePath]   string? __reserved_1 = default,
                   [CallerMemberName] string? __reserved_2 = default,
                   [CallerLineNumber] int __reserved_3     = default)
    {
        if (string.IsNullOrWhiteSpace(this.Name = Name))
            throw new ArgumentNullException(nameof(Name));

        if (!string.IsNullOrEmpty(Description))
            this.Description = Description;

        this.Source = new StackInfo() {
            File    = __reserved_1,
            Member  = __reserved_2,
            Line    = (uint)__reserved_3
        };
    }

    /// <summary>
    /// The name of the command.
    /// </summary>
    internal override string Name
    {
        get => base.Name;
        set => base.Name = value;
    }

    /// <summary>
    /// The description of the command.
    /// </summary>
    public override string Description
    {
        get => base.Description;
        set => base.Description = value;
    }
}