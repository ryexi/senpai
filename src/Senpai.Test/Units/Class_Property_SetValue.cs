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
    public void Prop_Should_Be_Null()
    {
        // Property should be null.
        SetValue(nameof(Prop), null);
        Assert.AreEqual(null, Prop);
    }

    [TestMethod]
    public void Prop_Should_Have_Value()
    {
        // Property should have a value.
        SetValue(nameof(Prop), 999);
        Assert.AreEqual(999, Prop);
    }

    [TestMethod]
    public void Should_Throw_If_A_Bad_Value_Is_Set_To_Prop()
    {
        // Writing a diff value type.
        Assert.ThrowsException<InvalidCastException>(() => SetValue(nameof(Prop), new object()));
    }

    [TestMethod]
    public void Should_Throw_If_PropNotWritable_Is_Written_To()
    {
        // Writing to a closed prop.
        Assert.ThrowsException<InvalidOperationException>(() => SetValue(nameof(PropNotWritable), null));
    }

    private void SetValue(string propName, object? value) 
        => CommandHandler.SetValue(this, GetType().GetProperty(propName)!, value);
}