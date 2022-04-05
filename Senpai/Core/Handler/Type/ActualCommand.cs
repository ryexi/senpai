using System.Reflection;
namespace Senpai.Core;

internal static partial class Handler
{
    public struct ActualCommand
    {
	    public Command Data;
        public MethodInfo Method;
        public object?[]? Parameters;
    }
}