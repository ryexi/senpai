namespace Senpai.Test.Units;

[TestClass]
public class Invocation_Tests
{
    [TestMethod]
    public void Thread_Should_Not_Block_Env_ExitCode()
    {
        Assert.IsTrue(Internal.InvokeProcess("exit") == 0);
        Assert.IsTrue(Internal.InvokeProcess("exit 0") == 0);
        Assert.IsTrue(Internal.InvokeProcess("exit 1") == 1);
        Assert.IsTrue(Internal.InvokeProcess("exit -69") == -69);
    }

    [TestMethod]
    public void Should_Return_Non_Zero_If_Command_Doesnt_Exist()
    {
        Assert.IsTrue(Internal.InvokeProcess(nameof(Should_Return_Non_Zero_If_Command_Doesnt_Exist)) != 0);
    }
}