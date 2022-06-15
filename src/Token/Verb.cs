using System.Runtime.CompilerServices;
using static Senpai.Exception;

namespace Senpai.Token;

[AttributeUsage(AttributeTargets.Method,
                AllowMultiple = false,
                Inherited = false)]
public sealed class Verb : Symbol
{
    /// <summary>
    /// Declare a method as a verb/subcommand.
    /// </summary>
    /// <param name="Name">The name of the command.</param>
    /// <param name="Description">The description of the command.</param>
    /// <param name="Assignee">The name of the parent command.</param>
    /// <param name="__reserved_1"></param>
    /// <param name="__reserved_2"></param>
    /// <param name="__reserved_3"></param>
    public Verb(string Name,
                string? Description,
                string Assignee,
                [CallerFilePath] string? __reserved_1 = default,
                [CallerMemberName] string? __reserved_2 = default,
                [CallerLineNumber] int __reserved_3 = default)
    {
        if (string.IsNullOrWhiteSpace(this.Name = Name))
            throw new ArgumentNullException(nameof(Name));

        if (!string.IsNullOrEmpty(Description))
            this.Description = Description;

        if (string.IsNullOrWhiteSpace(this.Assignee = Assignee))
            throw new ArgumentNullException(nameof(Assignee));

        this.Source = new StackInfo()
        {
            File = __reserved_1,
            Member = __reserved_2,
            Line = (uint)__reserved_3
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

    /// <remarks>
    /// The value should be the name given to the <see cref="Senpai.Token.Command"/> attribute.
    /// </remarks>
    internal string Assignee
    {
        get;
        set;
    }
}