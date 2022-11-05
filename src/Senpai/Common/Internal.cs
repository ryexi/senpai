namespace Senpai;

/// <summary>
/// Provides higher-level services (static methods) for <see langword="Senpai"/>.
/// </summary>
internal static class Internal
{
    public static void Error(string message) => 
        WriteLine(() => Console.Error.WriteLine($"Exception thrown: {message}"), ConsoleColor.Red, true, -1);

    public static void Throw(Type @class, string message) => 
        Error($"{message}\r\n   at {@class.FullName?.Replace('+', '.')}\r\n{Environment.StackTrace}");

    private static void WriteLine(Action execute,
                                  ConsoleColor? key, bool exit, int code = 0)
    {
        if (key != null)
        {
            Console.ForegroundColor = key.Value;
        }

        if (execute != null)
        {
            execute.Invoke();
        }

        Console.ResetColor();

        if (exit)
        {
            Environment.Exit(code);
        }
    }
}