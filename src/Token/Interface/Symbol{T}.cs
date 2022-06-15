namespace Senpai.Token;

public abstract class Symbol<T> : Symbol
{
    internal virtual Type GetGenericType()
    {
        return typeof(T);
    }
}