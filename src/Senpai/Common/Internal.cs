namespace Senpai;

/// <summary>
/// Provides higher-level services (static methods) for <see langword="Senpai"/>.
/// </summary>
internal static class Internal
{
    public static void Error(string message) => WriteError($"Exception thrown: {message}");

    public static void Error(Type @class, string message) => WriteError($"at {@class.FullName?.Replace('+', '.')}: {message}");

    private static void WriteError(string message)
    {
        WriteLine(() => Console.Error.WriteLine(message), ConsoleColor.Red);
        Environment.Exit(-1);
    }

    private static void WriteLine(Action execute, ConsoleColor? key)
    {
        Console.ForegroundColor = key ?? Console.ForegroundColor;
        execute?.Invoke();
        Console.ResetColor();
    }
}