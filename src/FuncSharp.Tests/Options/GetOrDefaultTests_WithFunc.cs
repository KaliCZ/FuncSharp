using System;
using FsCheck;
using FsCheck.Xunit;
using FuncSharp.Tests.Generative;
using Xunit;

namespace FuncSharp.Tests.Options;

public class GetOrDefaultTests_WithFunc
{
    public GetOrDefaultTests_WithFunc()
    {
        Arb.Register<OptionGenerators>();
    }

    [Fact]
    public void GetOrDefault_WithFunc()
    {
        var emptyOption = Option.Empty<object>();
        var valuedOption = Option.Create(Unit.Value);

        Assert.Equal(42, valuedOption.GetOrNull(_ => 42));
        Assert.Equal("asd", valuedOption.GetOrNull(_ => "asd"));
        Assert.Null(valuedOption.GetOrNull(_ => (string?)null));

        Assert.Equal(0, emptyOption.GetOrNull(_ => 14));
        Assert.Null(emptyOption.GetOrNull(_ => (int?)14));
        Assert.Null(emptyOption.GetOrNull(_ => "asd"));
    }

    [Property]
    internal void GetOrDefault_WithFunc_int(int value)
    {
        AssertGetOrDefaultWithFunc(value, i => i * 2);
    }

    [Property]
    internal void GetOrDefault_WithFunc_decimal(decimal value)
    {
        AssertGetOrDefaultWithFunc(value, d => d * 2);
    }

    [Property]
    internal void GetOrDefault_WithFunc_double(double value)
    {
        AssertGetOrDefaultWithFunc(value, d => d * 2);
    }

    [Property]
    internal void GetOrDefault_WithFunc_bool(bool value)
    {
        AssertGetOrDefaultWithFunc(value, b => !b);
    }

    [Property]
    internal void GetOrDefault_WithFunc_ReferenceType(ReferenceType value)
    {
        AssertGetOrDefaultWithFunc(value, d => new ReferenceType(d.Value * 2));

        // Also mapping to a struct
        AssertGetOrDefaultWithFunc(value, d => d.Value * 2);
    }

    private void AssertGetOrDefaultWithFunc<T, TResult>(T value, Func<T, TResult> map)
    {
        var option = Option.Valued(value);
        Assert.Equal(map(value), option.GetOrNull(map));

        Assert.Equal(default(TResult), Option.Empty<T>().GetOrNull(map));
    }
}