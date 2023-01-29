namespace Maid;

internal static class IEnumerableExtensions
{
    /// <summary>
    /// Returns the types which are derived from <see cref="Command"/>.
    /// </summary>
    public static IEnumerable<Type> WhereCommand(this IEnumerable<Type> types, bool isNested = false)
    {
        return types.Where(t =>
        {
            if (t.IsClass && t.BaseType == typeof(Command) && t.IsNested == isNested)
                return true;
            else
                return false;
        });
    }
}