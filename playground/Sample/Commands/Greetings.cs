namespace Sample.Commands
{
    public sealed class Greetings : Command
    {
        [Argument(1, "Name")]
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