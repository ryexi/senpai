namespace Sample.Commands
{
    public sealed class Goodbye : Command
    {
        [Argument(1)]
        public string? Name
        {
            get;
            set;
        }

        [Option("--flag", "Toggles something.")]
        public bool Flag
        {
            get;
            set;
        }

        protected override CommandProperty Properties => new()
        {
            Name = "bye",
            Description = "Displays a goodbye message followed by a name."
        };

        protected override void Invocation(object?[] args)
        {
            Console.WriteLine("Goodbye, {0}.", Name);
        }
    }
}