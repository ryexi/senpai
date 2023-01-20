namespace Senpai.Test.Units;

[TestClass]
public class Thread_Should_Not_Block_Env_ExitCode
{
    [TestMethod]
    public void Test()
    {
        Assert.IsTrue(Internal.InvokeProcess("exit") == 0);
        Assert.IsTrue(Internal.InvokeProcess("exit 0") == 0);
        Assert.IsTrue(Internal.InvokeProcess("exit 1") == 1);
        Assert.IsTrue(Internal.InvokeProcess("exit -69") == -69);
    }
}