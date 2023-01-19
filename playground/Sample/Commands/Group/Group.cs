namespace Sample.Commands
{
    public sealed partial class Group : Command
    {
        protected override bool HasHandler => false;

        protected override void Invocation(object?[] args)
        {
            throw new NotImplementedException();
        }
    }
}