using FsCheck;
using FsCheck.Xunit;
using FuncSharp.Tests.Generative;
using Xunit;

namespace FuncSharp.Tests.Objects;

public class MapTests
{
    [Fact]
    public void ReferenceType_MapTest()
    {
        string? notNullValue = "14";
        string? nullValue = null;

        Assert.Null(notNullValue.Map(v => (string?)null));
        Assert.Equal("28", notNullValue.Map(v => (int.Parse(v) * 2).ToString()));
        Assert.Equal(14, notNullValue.Map<string, int>(v => int.Parse(v)));
        Assert.Null(notNullValue.Map(v => (int?)null));

        Assert.Null(nullValue.Map(v => (string?)null));
        Assert.Null(nullValue.Map(v => "asdf"));
        Assert.Null(nullValue.Map<string, int>(v => int.Parse(v)));
        Assert.Null(nullValue.Map(v => (int?)null));
    }

    [Fact]
    public void ValueType_MapTest()
    {
        int? notNullValue = 14;
        int? nullValue = null;

        Assert.Null(notNullValue.Map(v => (string?)null));
        Assert.Equal("14", notNullValue.Map(v => v.ToString()));
        Assert.Equal(28, notNullValue.Map(v => v * 2));
        Assert.Null(notNullValue.Map(v => (int?)null));

        Assert.Null(nullValue.Map(v => (string?)null));
        Assert.Null(notNullValue.Map(v => v.ToString()));
        Assert.Null(notNullValue.Map(v => v * 2));
        Assert.Null(nullValue.Map(v => (int?)null));
    }
}