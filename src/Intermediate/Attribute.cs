using System.Reflection;
using A = System.Attribute;

namespace Senpai;

internal static partial class Intermediate
{
    public static class Attribute
    {
        /// <summary>
        /// Converts a generic attribute to its underlying type.
        /// </summary>
        public static dynamic ConvertType(A _Attribute, Type _AttributeGenericType)
        {
            var UnderlyingType = Intermediate.GetUnderlyingType(_Attribute.GetType())!;
            var GenericType = _AttributeGenericType.MakeGenericType(UnderlyingType);
            return (dynamic)Convert.ChangeType(_Attribute, GenericType);
        }

        public static A[] GetGenericAttributes(MemberInfo _Member, Type _Type)
        {
            return _Member.GetCustomAttributes().Where(a =>
            {
                var AttributeType = a.GetType();
                return AttributeType.IsGenericType ? AttributeType.GetGenericTypeDefinition() == _Type : false;

            }).ToArray();
        }
    }
}