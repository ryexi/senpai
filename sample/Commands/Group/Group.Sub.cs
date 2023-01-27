namespace Sample.Commands
{
    public sealed partial class Group
    {
        public sealed class Sub : Command
        {
            protected override void Invocation(object?[] args)
            {
                Console.WriteLine("A sub command of a command.");
            }
        }
    }
}