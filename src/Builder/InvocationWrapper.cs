using System.Reflection;

namespace Senpai;

internal static class InovacationWrapper
{
#if DEBUG
    internal static void Generate()
    {
        string t;
        string a = t = string.Empty;
        for (int i = 0; i < 15; i++)
        {
            if (i > 0)
            {
                t += ", T{i + 1}";
                a += ", A{i + 1}";
            }
            else
            {
                t += $"T{i + 1}";
                a += $"A{i + 1}";
            }

            Console.WriteLine($"public static Action<{t}> Wrap<{t}>(MethodInfo method) => new Action<{t}>(({a}) => method.Invoke(null, new object?[] {{ {a} }}));");
        }
    }
#endif

    public static Func<object?> Wrap(object? obj) => () => obj;

    public static Action<T1> Wrap<T1>(MethodInfo method) => new Action<T1>((A1) => method.Invoke(null, new object?[] { A1 }));

    public static Action<T1, T2> Wrap<T1, T2>(MethodInfo method) => new Action<T1, T2>((A1, A2) => method.Invoke(null, new object?[] { A1, A2 }));

    public static Action<T1, T2, T3> Wrap<T1, T2, T3>(MethodInfo method) => new Action<T1, T2, T3>((A1, A2, A3) => method.Invoke(null, new object?[] { A1, A2, A3 }));

    public static Action<T1, T2, T3, T4> Wrap<T1, T2, T3, T4>(MethodInfo method) => new Action<T1, T2, T3, T4>((A1, A2, A3, A4) => method.Invoke(null, new object?[] { A1, A2, A3, A4 }));

    public static Action<T1, T2, T3, T4, T5> Wrap<T1, T2, T3, T4, T5>(MethodInfo method) => new Action<T1, T2, T3, T4, T5>((A1, A2, A3, A4, A5) => method.Invoke(null, new object?[] { A1, A2, A3, A4, A5 }));

    public static Action<T1, T2, T3, T4, T5, T6> Wrap<T1, T2, T3, T4, T5, T6>(MethodInfo method) => new Action<T1, T2, T3, T4, T5, T6>((A1, A2, A3, A4, A5, A6) => method.Invoke(null, new object?[] { A1, A2, A3, A4, A5, A6 }));

    public static Action<T1, T2, T3, T4, T5, T6, T7> Wrap<T1, T2, T3, T4, T5, T6, T7>(MethodInfo method) => new Action<T1, T2, T3, T4, T5, T6, T7>((A1, A2, A3, A4, A5, A6, A7) => method.Invoke(null, new object?[] { A1, A2, A3, A4, A5, A6, A7 }));

    public static Action<T1, T2, T3, T4, T5, T6, T7, T8> Wrap<T1, T2, T3, T4, T5, T6, T7, T8>(MethodInfo method) => new Action<T1, T2, T3, T4, T5, T6, T7, T8>((A1, A2, A3, A4, A5, A6, A7, A8) => method.Invoke(null, new object?[] { A1, A2, A3, A4, A5, A6, A7, A8 }));

    public static Action<T1, T2, T3, T4, T5, T6, T7, T8, T9> Wrap<T1, T2, T3, T4, T5, T6, T7, T8, T9>(MethodInfo method) => new Action<T1, T2, T3, T4, T5, T6, T7, T8, T9>((A1, A2, A3, A4, A5, A6, A7, A8, A9) => method.Invoke(null, new object?[] { A1, A2, A3, A4, A5, A6, A7, A8, A9 }));

    public static Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> Wrap<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(MethodInfo method) => new Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>((A1, A2, A3, A4, A5, A6, A7, A8, A9, A10) => method.Invoke(null, new object?[] { A1, A2, A3, A4, A5, A6, A7, A8, A9, A10 }));

    public static Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> Wrap<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(MethodInfo method) => new Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>((A1, A2, A3, A4, A5, A6, A7, A8, A9, A10, A11) => method.Invoke(null, new object?[] { A1, A2, A3, A4, A5, A6, A7, A8, A9, A10, A11 }));

    public static Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> Wrap<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(MethodInfo method) => new Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>((A1, A2, A3, A4, A5, A6, A7, A8, A9, A10, A11, A12) => method.Invoke(null, new object?[] { A1, A2, A3, A4, A5, A6, A7, A8, A9, A10, A11, A12 }));

    public static Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> Wrap<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(MethodInfo method) => new Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>((A1, A2, A3, A4, A5, A6, A7, A8, A9, A10, A11, A12, A13) => method.Invoke(null, new object?[] { A1, A2, A3, A4, A5, A6, A7, A8, A9, A10, A11, A12, A13 }));

    public static Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> Wrap<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(MethodInfo method) => new Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>((A1, A2, A3, A4, A5, A6, A7, A8, A9, A10, A11, A12, A13, A14) => method.Invoke(null, new object?[] { A1, A2, A3, A4, A5, A6, A7, A8, A9, A10, A11, A12, A13, A14 }));

    public static Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> Wrap<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(MethodInfo method) => new Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>((A1, A2, A3, A4, A5, A6, A7, A8, A9, A10, A11, A12, A13, A14, A15) => method.Invoke(null, new object?[] { A1, A2, A3, A4, A5, A6, A7, A8, A9, A10, A11, A12, A13, A14, A15 }));

    public static Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> Wrap<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(MethodInfo method) => new Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>((A1, A2, A3, A4, A5, A6, A7, A8, A9, A10, A11, A12, A13, A14, A15, A16) => method.Invoke(null, new object?[] { A1, A2, A3, A4, A5, A6, A7, A8, A9, A10, A11, A12, A13, A14, A15, A16 }));
}