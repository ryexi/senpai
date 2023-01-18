using System.Reflection;

namespace Senpai.Converters;

internal sealed class ArgumentConverter
{
    public static ArgumentProperty[] Convert(PropertyInfo[] props)
    {
        var pool = new List<ArgumentProperty>();

        foreach (var prop in props)
        {
            if (Attribute.IsDefined(prop, typeof(ArgumentAttribute)))
            {
                pool.Add(new ArgumentProperty(prop));
            }
        }

        return pool.OrderBy(p => p.Position).ToArray();
    }
}