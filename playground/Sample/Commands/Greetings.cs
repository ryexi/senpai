namespace Sample.Commands
{
    public sealed class Greetings : Command
    {
        [Argument(1)]
        public string? Name
        {
            get;
            set;
        }

        protected override void Invocation(object?[] args)
        {
            Console.WriteLine("Greetings, {0}.", Name);
        }
    }
}