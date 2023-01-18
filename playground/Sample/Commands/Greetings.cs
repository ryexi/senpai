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

        protected override int Invocation(object?[] args)
        {
            Console.WriteLine("Greetings, {0}.", Name);
            return 0;
        }
    }
}