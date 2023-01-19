using Senpai.Converter;
using Senpai.Invocation;
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
        Properties ??= new()
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
        if (HasHandler)
        {
            this.SetHandler();
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
    /// Gets or sets a value indicating whether the command is executable or not.
    /// </summary>
    protected virtual bool HasHandler => true;

    /// <summary>
    /// The properties of the <see cref="Command"/>.
    /// </summary>
    /// <remarks>
    /// Override this property to change the name and the description of the <see cref="Command">command</see>.
    /// </remarks>
    protected virtual CommandProperty Properties
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

    /// <summary>
    /// This method serves as the starting point for <see cref="Command">command</see> execution.
    /// </summary>
    /// <param name="args">The <see cref="ArgumentAttribute">arguments</see> that were passed to the parent <see cref="Command">command</see> and its predecessors.
    /// </param>
    protected internal abstract void Invocation(object?[] args);

    private ArgumentProperty[] GetArgumentProperties() => ArgumentConverter.Convert(Owner.GetProperties());

    private OptionProperty[] GetOptionProperties() => OptionConverter.Convert(Owner.GetProperties());
}