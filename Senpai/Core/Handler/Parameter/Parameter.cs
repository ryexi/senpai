using System.Reflection;
namespace Senpai.Core;

internal static partial class Handler
{
    /// <summary>
    /// The parameter builder/handler.
    /// </summary>
    internal static partial class Parameter
    {
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

                if (IsType<string>(ptype))
                {
                    result[i] = Arguments[i];
                    continue;
                }

                if (IsType<char>(ptype))
                {
                    result[i] = Converter.ToInt(Arguments[i], i + 1);
                    continue;
                }

                if (IsType<int>(ptype))
                {
                    result[i] = Converter.ToInt(Arguments[i], i + 1);
                    continue;
                }

                if (IsType<long>(ptype))
                {
                    result[i] = Converter.ToInt(Arguments[i], i + 1);
                    continue;
                }

                if (IsType<double>(ptype))
                {
                    result[i] = Converter.ToDouble(Arguments[i], i + 1);
                    continue;
                }

                if (IsType<bool>(ptype))
                {
                    result[i] = Converter.ToBoolean(Arguments[i], i + 1);
                    continue;
                }
            }

            return result;
        }

        static bool IsType<T>(Type type)
        {
            return type == typeof(T) || Nullable.GetUnderlyingType(type) == typeof(T);
        }

        static bool IsNullable(Type type)
        {
            return Nullable.GetUnderlyingType(type) != null;
        }

        static bool IsSupported(Type type)
        {
            return IsType<string>(type) || IsType<int>(type) || IsType<double>(type) || IsType<bool>(type) || IsType<char>(type) || IsType<long>(type);
        }
    }
}