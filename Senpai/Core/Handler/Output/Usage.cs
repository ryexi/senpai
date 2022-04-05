using System.Text;
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
                    var name = GetName(parameters[i].Name);
                    var typeName = GetTypeName(parameters[i].ParameterType);

                    // output += $"[arg{i + 1}] ";
                    output += $"[{name}: {typeName}] ";
                }

                Console.WriteLine(
                    output
                );

                Helper.ExitIf(exit);
            }

            static string GetName(string? name)
            {
                // Obfuscation issues?
                if (string.IsNullOrWhiteSpace(name) || 
                    Encoding.ASCII.GetString(Encoding.ASCII.GetBytes(name)).Contains("?"))
                    return "arg";
                else
                    return name;
            }

            static string GetTypeName(Type type)
            {
                if (Nullable.GetUnderlyingType(type) != null)
                    return GetUnderlyingType(type);
                else
                    return type.Name;
            }

            static string GetUnderlyingType(Type type)
            {
                return Nullable.GetUnderlyingType(type)?.Name ?? type.Name;
            }
        }
    }
}