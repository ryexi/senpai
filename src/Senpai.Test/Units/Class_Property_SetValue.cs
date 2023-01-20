using Senpai.Invocation;

namespace Senpai.Test.Units;

[TestClass]
public class Class_Property_SetValue
{
    public int? Prop
    {
        get;
        set;
    }

    public string? PropNotWritable
    {
        get;
    }

    [TestMethod]
    public void Test()
    {
        // Property should be null.
        SetValue(nameof(Prop), null);
        Assert.AreEqual(Prop, null);

        // Property should have a value.
        SetValue(nameof(Prop), 999);
        Assert.AreEqual(Prop, 999);

        // Writing to a closed prop.
        SetValue(nameof(PropNotWritable), new object());
        Assert.AreEqual(PropNotWritable, null);
    }

    private void SetValue(string propName, object? value) 
        => CommandHandler.SetValue(this, GetType().GetProperty(propName), value);
}