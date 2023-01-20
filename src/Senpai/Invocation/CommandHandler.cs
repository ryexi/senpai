using System.Reflection;

namespace Senpai.Invocation;

internal sealed class CommandHandler
{
    private CommandHandlerContext _context;
    private SafeThread _thread;

    public CommandHandler(CommandHandlerContext context)
    {
        _context = context;
        _thread = new SafeThread(Initialize);
    }

    public int ExitCode
    {
        get => _context.InvocationContext.ExitCode;
        set => _context.InvocationContext.ExitCode = value;
    }

    public void Invoke(object?[] args)
    {
        try
        {
            _context.CommandContext.Invocation(args);
        }
        catch (Exception)
        {
            ExitCode = -1;
            throw;
        }
    }

    internal static void SetValue(object target, PropertyInfo prop, object? value)
    {
        if (prop is null)
        {
            throw new InvalidOperationException(
                $"{nameof(prop)} shouldn't be null here."
            );
        }

        if (!prop.CanWrite)
        {
            throw new InvalidOperationException(
                $"Cannot write to property at '{prop.DeclaringType}.{prop.Name}'."
            );
        }
        else
        {
            if (value != null && !IsValid(prop.PropertyType, value.GetType()))
            {
                throw new InvalidCastException(
                    $"The value being set doesn't match its property's type. Cannot convert '{prop.PropertyType}' to '{value.GetType()}'."
                );
            }

            //if (value == null && Nullable.GetUnderlyingType(prop.PropertyType) == null)
            //{
            //}
        }

        prop.SetValue(target, value);
    }

    private static bool IsNullable(Type type, out Type? baseType) => (baseType = Nullable.GetUnderlyingType(type)) != null;

    /// <summary>
    /// Returns <see langword="false"/> if the validation fails.
    /// </summary>
    private static bool IsValid(Type source, Type target)
    {
        if (IsNullable(source, out var underlyingType) && underlyingType == target)
            return true;

        if (source == target)
            return true;

        return false;
    }

    /// <summary>
    /// While in the context of <see cref="System.CommandLine"/>, codes running here is considered as the command's execution codes, 
    /// but, in the context of <see cref="Senpai"/>, codes running here should be considered as pre-execution codes until <see cref="Invoke(object?[])"/> is actually invoked.
    /// </summary>
    private void Initialize()
    {
        var context = _context.InvocationContext;
        var command = _context.CommandContext;
        var @params = new List<object?>();

        foreach (var arg in command.GetParentArguments())
        {
            @params.Add(context.ParseResult.GetValue(arg.Argument));
        }

        for (int i = 0; i < command.Arguments?.Length; i++)
        {
            var arg = command.Arguments[i];
            SetValue(command, arg.Property, context.ParseResult.GetValue(arg.Argument));
        }

        for (int i = 0; i < command.Options?.Length; i++)
        {
            var arg = command.Options[i];
            SetValue(command, arg.Property, context.ParseResult.GetValue(arg.Argument));
        }

        this.Invoke(@params.ToArray());
    }
}