using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senpai.Core;

internal static partial class Handler
{
    /// <summary>
    /// The output displayed when no inputs were given. Default behavior.
    /// </summary>
    internal static class Welcome
    {
        public static void Display()
        {
            if (!string.IsNullOrWhiteSpace(Default))
            {
                Console.WriteLine(
                    Default
                );
                return;
            }

            Console.WriteLine(
                "Run the application with the '--help' flag to display all available commands."
            );
        }
    }
}
