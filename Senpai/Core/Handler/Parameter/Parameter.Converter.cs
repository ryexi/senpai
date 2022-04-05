namespace Senpai.Core;

internal static partial class Handler
{
    internal static partial class Parameter
    {
        internal static class Converter
        {
            private static string ExceptionMessage(string value, int index) => $"'{value}' is not a valid value at [arg{index}].";

            public static char ToChar(string value, int pos)
            {
                char output;
                return char.TryParse(value, out output) ? output : throw new Senption(ExceptionMessage(value, pos), true);
            }
            
            public static int ToInt(string value, int pos)
            {
                int output;
                return int.TryParse(value, out output) ? output : throw new Senption(ExceptionMessage(value, pos), true);
            }

            public static long ToLong(string value, int pos)
            {
                long output;
                return long.TryParse(value, out output) ? output : throw new Senption(ExceptionMessage(value, pos), true);
            }

            public static double ToDouble(string value, int pos)
            {
                double output;
                return double.TryParse(value, out output) ? output : throw new Senption(ExceptionMessage(value, pos), true);
            }

            public static bool ToBoolean(string value, int pos)
            {
                try
                {
                    return Convert.ToBoolean(value);
                }
                catch (FormatException)
                {
                    throw new Senption(ExceptionMessage(value, pos), true);
                }
            }
        }
    }
}