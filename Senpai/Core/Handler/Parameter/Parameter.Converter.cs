namespace Senpai.Core;

internal static partial class Handler
{
    internal static partial class Parameter
    {
        internal static class Converter
        {
            public static int ToInt(string value, int pos)
            {
                int output;
                var result = int.TryParse(value, out output);
                if (result)
                    return output;
                else
                    throw new Senption($"'{value}' is not a valid value (arg{pos}).", true);
            }

            /*
            public static T To<T>(T value)
            {
                T output;

            }
            */

            public static double ToDouble(string value, int pos)
            {
                double output;
                var result = double.TryParse(value, out output);
                if (result)
                    return output;
                else
                    throw new Senption($"'{value}' is not a valid value (arg{pos}).", true);
            }
        }
    }
}