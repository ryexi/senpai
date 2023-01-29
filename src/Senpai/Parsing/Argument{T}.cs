using System.CommandLine;

namespace Maid.Parsing;

internal static class Argument<T> where T : SymbolAttribute
{
    private static Type GetAppropriateType => typeof(T) == typeof(ArgumentAttribute) ? typeof(System.CommandLine.Argument<>) : typeof(Option<>);

    public static Symbol Create(Type type, string name)
    {
        var genericType = GetAppropriateType.MakeGenericType(type);
        return (Symbol)Activator.CreateInstance(genericType, new object[] { name, null });
    }
}