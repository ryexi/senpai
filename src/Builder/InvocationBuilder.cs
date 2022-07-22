using System.CommandLine;
using System.CommandLine.Binding;
using System.Reflection;

namespace Senpai.Builder;

internal static class InvocationBuilder
{
    public static void Build(Command Command, IValueDescriptor[] Params, MethodInfo Method)
    {
        var Parameters     = Method.GetParameters();
        var HandlerMethods = typeof(System.CommandLine.Handler).GetMethods();
        var GenericWrapper = typeof(MethodWrapper).GetMethods().Where(m => m.GetGenericArguments().Length == Parameters.Length).ToArray().FirstOrDefault()!;
        
        //! Hypothetically, this shit might crash.
        //! There might be a chance of this not grabbing the Action<> but instead the Func<>.
        //  Todo Fix this.
        for (int i = 0; i < HandlerMethods.Length; i++)
        {
            var GenericMethod = HandlerMethods[i];

            if (GenericMethod.GetGenericArguments().Length != Parameters.Length)
                continue;

            Type[] types = new Type[Parameters.Length];

            for (int p = 0; p < types.Length; p++)
                types[p] = Parameters[p].ParameterType;

            var Wrapper = GenericWrapper.MakeGenericMethod(types);
            var GenericAction = Wrapper.Invoke(null, new object[] { Method })!;
            
            GenericMethod.MakeGenericMethod(types).Invoke(null, new object[] { Command, GenericAction, Params });
            return;
        }
    }
}