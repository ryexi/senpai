namespace Senpai;

internal static class Helper
{
    public static void ExitIf(bool condition, int code = 0)
    {
        if (condition)
            Environment.Exit(code);
        else
            return;
    }

    public static void Error(string str) => WriteLine(str, ConsoleColor.Red);
    
    public static void Warning(string str) => WriteLine(str, ConsoleColor.Yellow);

    public static void Critical(string str)
    {
        Error(
            str
        );
        WriteLine(
            "\nThe process has been terminated due to an unexpected error."
        );
        Environment.Exit(-1);
    }

    public static void WriteLine(string str, ConsoleColor? color = null)
    {
        Console.ForegroundColor = color ?? Console.ForegroundColor;
        Console.WriteLine(str);
        Console.ResetColor();
    }
}