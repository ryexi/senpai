using System.Reflection;
namespace Senpai.Core;

internal static partial class Handler
{
    internal static partial class Parameter
    {
        static bool IsInt(Type type) => type == typeof(int) || IsNullable(type);

        static bool IsDouble(Type type) => type == typeof(double) || IsNullable(type);

        static bool IsString(Type type) => type == typeof(string);

        static bool IsNullable(Type type) => Nullable.GetUnderlyingType(type) != null;

        static bool IsSupported(Type type) => IsString(type) || IsInt(type) || IsDouble(type);

        // Todo Probably fix this
        public static object?[]? Build(MethodInfo method)
        {
            object?[]? result;
            var parameters = method.GetParameters();

            if ((result = new object[parameters.Length]).Length == 0)
                return null;

            int len = parameters.Length;
            int req = parameters.Where(p => !p.IsOptional).ToArray().Length;
            int arg = Arguments!.Length;

            // We have some params
            if (arg < req)
                throw new Senption($"Missing required parameters. ({req - arg})", true);

            for (int i = 0; i < len; i++)
            {
                var param = parameters[i];
                var ptype = param.ParameterType;

                if (!IsSupported(ptype))
                    throw new Exception("ParameterType is unsupported.");

                if (param.IsOptional)
                {
                    if ((i + 1) > arg)
                    {
                        result[i] = param.DefaultValue;
                        continue;
                    }
                }

                if (IsNullable(ptype))
                {
                    if (string.IsNullOrWhiteSpace(Arguments[i]))
                    {
                        result[i] = null;
                        continue;
                    }
                }

                if (IsString(ptype))
                {
                    result[i] = Arguments[i];
                    continue;
                }

                if (IsInt(ptype))
                {
                    result[i] = Converter.ToInt(Arguments[i], i + 1);
                    continue;
                }

                if (IsDouble(ptype))
                {
                    result[i] = Converter.ToDouble(Arguments[i], i + 1);
                    continue;
                }
            }

            return result;
        }
    }
}