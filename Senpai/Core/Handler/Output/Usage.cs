namespace Senpai.Core;

internal static partial class Handler
{
    internal static partial class Output
    {
        internal static class Usage
        {
            public static void Display(ActualCommand current, bool exit = false)
            {
                if (!string.IsNullOrWhiteSpace(current.Data.Usage))
                {
                    Console.WriteLine(
                        current.Data.Usage
                    );
                    return;
                }

                var parameters = current.Method.GetParameters();
                var output = current.Data.Name.ToUpper() + (char)32 + (parameters.Length == 0 ? "(no args)" : null);

                for (int i = 0; i < parameters.Length; i++)
                {
                    // output += $"<{parameters[i].ParameterType} arg{i}> ";
                    output += $"[arg{i + 1}] ";
                }

                Console.WriteLine(
                    output
                );

                Helper.ExitIf(exit);
            }
        }
    }
}