using System.CommandLine;
using System.Reflection;

namespace Senpai;

internal static partial class Intermediate
{
    public static ArgumentArity ConvertEnum(Token.ArgumentArity ArityEnum)
    {
        switch (ArityEnum)
        {
            case Token.ArgumentArity.Zero:
                return ArgumentArity.Zero;
            case Token.ArgumentArity.ZeroOrOne:
                return ArgumentArity.ZeroOrOne;
            case Token.ArgumentArity.ExactlyOne:
                return ArgumentArity.ExactlyOne;
            case Token.ArgumentArity.ZeroOrMore:
                return ArgumentArity.ZeroOrMore;
            case Token.ArgumentArity.OneOrMore:
                return ArgumentArity.OneOrMore;
            default:
                return default;
        }
    }

    /// <summary>
    /// Returns methods that contains an attribute of <see cref="System.Type"/>.
    /// </summary>
    /// <Remarks>Methods must have the 'public' and 'static' modifier.</Remarks>
    public static MethodInfo[] GetMethods(Assembly Caller, Type type)
    {
        return Caller?.GetTypes().SelectMany(t => t.GetMethods(BindingFlags.Public |
                                                               BindingFlags.Static |
                                                               BindingFlags.InvokeMethod))
                                  .Where(m => m.GetCustomAttributes(type, false).Length > 0)
                                  .ToArray() ?? new MethodInfo[0];
    }

    public static Type? GetUnderlyingType(Type type)
    {
        return type.IsGenericType ? type.GetGenericArguments().FirstOrDefault() : type;
    }

    public static Type[] GetUnderlyingTypes(Type type)
    {
        return type.IsGenericType ? type.GetGenericArguments() : new Type[] { type };
    }
}