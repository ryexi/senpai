using System.Text;
namespace Senpai.Core;

internal static partial class Handler
{
    internal static class Help
    {
        // Performance goes brrr
        public static void Display()
        {
            var Commands = Handler.GetCommands();
            var LongestName = GetLongestFromArray(NamesToArray(Commands));

            #region Flags
            /*
                Container.Append(
                    "Options:\n\t"
                );
            */
            #endregion

            #region Commands
            for (int i = 0; i < Commands?.Length; i++)
            {
                var Command = Commands[i];
                var Attribute = Command.Data;
                var CommandName = Attribute.Name;
                var Description = Attribute.Description;

                if (Attribute.Ignore)
                    continue;

                Console.WriteLine(
                    $"{CommandName.ToUpper()}{AddSpace(LongestName - CommandName.Length)}\t\t{Slice(Description, CommandName.Length + (LongestName - CommandName.Length)) ?? "No description provided."}"
                );
            }
            #endregion

            if (Commands?.Length > 0)
            {
                Console.WriteLine("\nRun '[command] --help' for more information on a command.");
            }
        }

        static int GetLongestFromArray(string[]? array)
        {
            if (array == null || array.Length == 0)
                return 0;
            return array.Max(w => w.Length);
        }

        static string[] NamesToArray(Handler.ActualCommand[]? context)
        {
            var array = new string[context != null ? context.Length : 0];
            for (int i = 0; i < context?.Length; i++)
                array[i] = context[i].Data.Name;
            return array;
        }

        static string AddSpace(int count)
        {
            string str = string.Empty;
            for (int i = 0; i < count; i++)
                str += (char)32;
            return str;
        }

        // Todo: It works.. well kinda.. fix please
        private static string? Slice(string? str, int space)
        {
            const int max_len = 60;

            if (string.IsNullOrEmpty(str))
                return null;

            // no slicing
            if (str.Length < max_len)
                return str;

            var count = 0;
            var result = string.Empty;

            for (int i = 0; i < str.Length; i++)
            {
                var chr = str[i];
                result += chr;

                if (count == max_len)
                {
                    // Todo: This part, especially.
                    if (chr == 32 && (str.Length - i) >= 10)
                    {
                        result += $"\n{AddSpace(space)}\t\t";
                        count = 0;
                    }
                    else
                    {
                        count--;
                    }
                }

                count++;
            }

            return result;
        }

        public static bool HasFlag(string[]? args = null)
        {
            args = args ?? Arguments;
            for (int i = 0; i < args?.Length; i++)
            {
                switch (args?[i].ToLower())
                {
                    case "/?":
                    case "/h":
                    case "-?":
                    case "-h":
                    case "--help":
                        return true;
                }
            }

            return false;
        }

    }
}