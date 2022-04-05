using System.Diagnostics;
using System.Reflection;
using System.Runtime.ExceptionServices;

namespace Senpai.Core;

internal static partial class Handler
{
    public static Assembly? Caller
    {
        get;
        private set;
    }

    public static string[]? Arguments
    {
        get;
        private set;
    }

    public static void Initialize(string[] args, Assembly caller)
    {
        ActualCommand Current;
        MethodInfo[]? RawCommands;

        if (args == null || (Caller = caller) == null)
        {
            throw new ArgumentNullException();
        }

        if (args.Length == 0)
        {
            Output.Default.Display();
            return;
        }

        if ((RawCommands = GetMethods()).Length == 0)
        {
            throw new Senption("No methods found with the command decorator.");
        }

        // Yoinking the first string from 'args' and initializing 'Arguments'.
        Array.Copy(args,
                   1,
                   (Arguments = new string[args.Length - 1]),
                   0,
                   Arguments.Length);

        #region Searching
        var HasFlag = Output.Help.HasFlag(args);

        if (ValidateCommand(args[0], ref RawCommands, out Current))
        {
            if (HasFlag)
            {
                Output.Usage.Display(Current, true);
                return;
            }
        }
        else
        {
            if (!HasFlag)
            {
                throw new Senption("Could not execute because the specified command was not found.");
            }
            else
            {
                Output.Help.Display();
                return;
            }
        }
        #endregion

        Execute(
            Current
        );
    }

    private static void Execute(ActualCommand cmd)
    {
        try
        {
            var Method = cmd.Method;
            var Parameters = cmd.Parameters = Parameter.Build(Method);

            Method.Invoke(null,
                          Parameters);
        }
        catch (TargetInvocationException e)
        {
            if (Debugger.IsAttached)
                // https://berserkerdotnet.github.io/blog/rethrow-exception-correctly-in-dotnet/
                ExceptionDispatchInfo.Capture(e.InnerException ?? e).Throw();
            else
                Helper.Critical((e.InnerException ?? e).ToString());
        }
        catch (Exception e)
        {
            if (!Debugger.IsAttached)
                Helper.Critical(e.ToString());
            throw;
        }
    }

    private static Command? GetAttribute(MethodInfo method)
    {
        return (Command?)Attribute.GetCustomAttribute((MemberInfo)method, typeof(Command));
    }

    private static MethodInfo[] GetMethods()
    {
        return Caller?.GetTypes()
                       .SelectMany(t => t.GetMethods(BindingFlags.Public |
                                                     BindingFlags.Static |
                                                     BindingFlags.InvokeMethod))
                       .Where(m => m.GetCustomAttributes(typeof(Command), false).Length > 0)
                       .ToArray() ?? new MethodInfo[0];
    }

    public static ActualCommand[]? GetCommands(MethodInfo[]? cmds = null)
    {
        var methods = cmds ?? GetMethods();
        var commands = new ActualCommand[methods.Length];

        for (int i = 0; i < commands.Length; i++)
        {
            commands[i].Data = GetAttribute(methods[i])!;
            commands[i].Method = methods[i];
        }

        return commands;
    }

    private static bool ValidateCommand(string name, ref MethodInfo[] methods, out ActualCommand command)
    {
        command = default;

        if (name == null || methods.Length == 0)
        {
            throw new ArgumentNullException();
        }

        for (int i = 0; i < methods.Length; i++)
        {
            var method = methods[i];
            var attribute = GetAttribute(method)!;

            if (String.Equals(name, attribute.Name, StringComparison.OrdinalIgnoreCase))
            {
                command = new ActualCommand()
                {
                    Data = attribute,
                    Method = method
                };

                return true;
            }
        }

        return false;
    }
}