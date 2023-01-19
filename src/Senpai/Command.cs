using Senpai.Parsing;
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
        UnderlyingCommand = CommandConverter.Create(this);

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

    internal string DefaultName => Owner.Name.ToLower().ToLower();

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
    /// The properties of the <see cref="Command">command</see>.
    /// </summary>
    /// <remarks>
    /// Override this property to change the name and the description of the <see cref="Command">command</see>.
    /// </remarks>
    protected internal virtual CommandProperty Properties => new()
    {
        Name = DefaultName,
        Description = Resources.SymbolNoDescriptionProvided
    };

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
    protected internal virtual void Invocation(object?[] args) {}

    /// <summary>
    /// Gets or sets a value indicating whether the command is executable or not.
    /// </summary>
    private bool HasHandler
    {
        get
        {
            var method = Invocation;
            var methodInfo = method.Method;

            return methodInfo.GetBaseDefinition().DeclaringType != methodInfo.DeclaringType;
        }
    }

    private ArgumentProperty[] GetArgumentProperties() => ArgumentConverter.Convert(Owner.GetProperties());

    private OptionProperty[] GetOptionProperties() => OptionConverter.Convert(Owner.GetProperties());
}