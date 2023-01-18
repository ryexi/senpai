using System.Reflection;

namespace Senpai.Converters;

internal sealed class OptionConverter
{
    public static OptionProperty[] Convert(PropertyInfo[] props)
    {
        var pool = new List<OptionProperty>();

        foreach (var prop in props)
        {
            if (Attribute.IsDefined(prop, typeof(OptionAttribute)))
            {
                pool.Add(new OptionProperty(prop));
            }
        }

        return pool.ToArray();
    }
}