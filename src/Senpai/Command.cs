using System.CommandLine;
using Senpai.Converters;
using Senpai.Properties;

namespace Senpai;

/// <summary>
/// Represents a specific action that the application performs.
/// </summary>
public abstract class Command
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Command"/> class.
    /// </summary>
    public Command()
    {
        Properties ??= new SymbolAttribute
        {
            Name = Owner.Name.ToLower(),
            Description = Resources.SymbolNoDescriptionProvided
        };

        UnderlyingCommand = new(Properties.Name,
                                Properties.Synopsis,
                                Properties.Description);

        // Add arguments
        foreach (var arg in Arguments = GetArgumentProperties())
        {
            UnderlyingCommand.Add(arg.Argument);
        }

        // Add options
        foreach (var opt in Options = GetOptionProperties())
        {
            UnderlyingCommand.Add(opt.Argument);
        }

        // SetHandler
        if (!IsAbsent)
        {
            UnderlyingCommand.SetHandler(
                (context) => new InvocationHandler(this, context)
            );
        }
    }

    internal ArgumentProperty[]? Arguments
    {
        get;
        set;
    }

    internal OptionProperty[]? Options
    {
        get;
        set;
    }

    internal Type Owner => GetType();

    internal object? ParentClass
    {
        get;
        set;
    }

    internal InternalCommand UnderlyingCommand
    {
        get;
        set;
    }

    /// <summary>
    /// Determines whether the command should be invoked or not.
    /// </summary>
    protected virtual bool IsAbsent
    {
        get;
        set;
    }

    /// <summary>
    /// The properties of the <see cref="Command"/>.
    /// </summary>
    /// <remarks>
    /// Override this property to change the name and the description of the <see cref="Command"/>.
    /// </remarks>
    protected virtual Symbol Properties
    {
        get;
        set;
    }

    internal ArgumentProperty[] GetParentArguments()
    {
        if (ParentClass is null || ParentClass is not Command command)
        {
            return Array.Empty<ArgumentProperty>();
        }

        return command.GetParentArguments().Union(command.Arguments ?? Array.Empty<ArgumentProperty>()).ToArray();
    }

    internal int Invoke(object?[] args) => Invocation(args);

    /// <summary>
    /// This method serves as the starting point for <see cref="Command"/> execution.
    /// </summary>
    /// <param name="args">The arguments passed to the parent <see cref="Command"/> and its predecessors.</param>
    /// <returns>
    /// The exit-code of the invocation.
    /// </returns>
    protected abstract int Invocation(object?[] args);

    private ArgumentProperty[] GetArgumentProperties() => ArgumentConverter.Convert(Owner.GetProperties());

    private OptionProperty[] GetOptionProperties() => OptionConverter.Convert(Owner.GetProperties());
}