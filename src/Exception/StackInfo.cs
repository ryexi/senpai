namespace Senpai;

internal partial class Exception
{
    internal struct StackInfo
    {
        public uint Line;
        public string? File;
        public string? Member;
    }
}