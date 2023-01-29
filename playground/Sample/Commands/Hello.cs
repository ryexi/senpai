namespace Sample.Commands
{
    public sealed class Hello : Command
    {
        protected override void Invocation(object?[] args)
        {
            Console.WriteLine("Hello World");
        }
    }
}