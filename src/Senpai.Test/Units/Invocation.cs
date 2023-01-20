namespace Senpai.Test.Units;

[TestClass]
public class Invocation
{
    [TestMethod]
    public void Thread_Should_Not_Block_Env_ExitCode()
    {
        Assert.IsTrue(Internal.InvokeProcess("exit") == 0);
        Assert.IsTrue(Internal.InvokeProcess("exit 0") == 0);
        Assert.IsTrue(Internal.InvokeProcess("exit 1") == 1);
        Assert.IsTrue(Internal.InvokeProcess("exit -69") == -69);
    }
}