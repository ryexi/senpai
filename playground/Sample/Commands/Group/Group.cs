namespace Sample.Commands
{
    public sealed partial class Group : Command
    {
        protected override bool IsAbsent => true;

        protected override int Invocation(object?[] args)
        {
            throw new NotImplementedException();
        }
    }
}