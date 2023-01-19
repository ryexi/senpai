using System.Reflection;

namespace Senpai.Parsing;

internal sealed class ArgumentConverter
{
    public static ArgumentProperty[] Convert(PropertyInfo[] props)
    {
        var pool = new List<ArgumentProperty>();

        foreach (var prop in props)
        {
            if (Attribute.IsDefined(prop, typeof(ArgumentAttribute)))
            {
                pool.Add(Parse(prop));
            }
        }

        return pool.OrderBy(p => p.Position).ToArray();
    }

    private static ArgumentProperty Parse(PropertyInfo propInfo)
    {
        var attribute = propInfo.GetCustomAttribute<ArgumentAttribute>().ShouldNotBeNull();
        var symbol    = Argument<ArgumentAttribute>.Create(propInfo.PropertyType, attribute.Name ?? propInfo.Name);

        symbol.SetValues(attribute);

        return new ArgumentProperty(propInfo, attribute, (System.CommandLine.Argument)symbol);
    }
}