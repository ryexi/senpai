using System.Reflection;

namespace Senpai.Operation;

internal static class Reflection
{
    public static Type[] GetStaticTopLevelClasses(Assembly caller)
    {
        return caller?.ExportedTypes.Where(t => t.IsClass && !t.IsNested && t.IsAbstract && t.IsSealed).ToArray() ?? new Type[0];
    }

    public static Type[] GetClassesWithAttribute(Type[] classTypes, Type attributeType)
    {
        return classTypes.Where(t => t.GetCustomAttributes(attributeType, false).Length > 0).ToArray();
    }

    public static Attribute[] GetGenericAttributes(MemberInfo member, Type type)
    {
        return member.GetCustomAttributes().Where(a =>
        {
            var AttributeType = a.GetType();
            return AttributeType.IsGenericType ? AttributeType.GetGenericTypeDefinition() == type : false;

        }).ToArray();
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