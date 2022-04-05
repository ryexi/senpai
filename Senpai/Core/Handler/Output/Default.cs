namespace Senpai.Core;

internal static partial class Handler
{
    internal static partial class Output
    {
        /// <summary>
        /// The output displayed when no inputs were given. Default behavior.
        /// </summary>
        internal static class Default
        {
            public static string? Buffer
            {
                get;
                set;
            }

            public static void Display()
            {
                if (!string.IsNullOrWhiteSpace(Buffer))
                {
                    Console.WriteLine(
                        Buffer
                    );
                    return;
                }

                Console.WriteLine(
                    "Run the application with the '--help' flag to display all available commands."
                );
            }
        }
    }
}
