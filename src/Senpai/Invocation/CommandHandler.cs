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

    private void SetValue(object target, PropertyInfo prop, object? value)
    {
        if (value is null)
            throw new ArgumentNullException(nameof(value));

        if (!prop.CanWrite)
            throw new Exception($"Property '{prop.DeclaringType}.{prop.Name}' cannot be written to.");

        prop.SetValue(target, value);
    }
}