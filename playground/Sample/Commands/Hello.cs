namespace Sample.Commands
{
    public sealed class Hello : Command
    {
        protected override int Invocation(object?[] args)
        {
            Console.WriteLine("Hello World");
            return 0;
        }
    }
}