namespace Senpai;

/// <summary>
/// Provides extension methods.
/// </summary>
internal static partial class Extension
{
    public static Type? GetUnderlyingType(this Type type)
    {
        return type.IsGenericType ? type.GetGenericArguments().FirstOrDefault() : type;
    }

    public static Type[] GetUnderlyingTypes(this Type type)
    {
        return type.IsGenericType ? type.GetGenericArguments() : new Type[] { type };
    }
}