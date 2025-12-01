using FsCheck;
using FsCheck.Xunit;
using FuncSharp.Tests.Generative;
using Xunit;

namespace FuncSharp.Tests.Options;

public class ToOptionTests
{
    public ToOptionTests()
    {
        Arb.Register<OptionGenerators>();
    }

    [Fact]
    public void ToOption()
    {
        OptionAssert.NonEmptyWithValue(3, 3.ToValuedOption());
        OptionAssert.NonEmptyWithValue(0, 0.ToValuedOption());
        OptionAssert.NonEmptyWithValue(2, ((int?)2).ToOption());
        OptionAssert.NonEmptyWithValue(new ReferenceType(13), new ReferenceType(13).ToOption());
        OptionAssert.NonEmptyWithValue(Unit.Value, Unit.Value.ToOption());

        OptionAssert.IsEmpty(((int?)null).ToOption());
        OptionAssert.IsEmpty(((ReferenceType?)null).ToOption());
        OptionAssert.IsEmpty(((Unit?)null).ToOption());
    }

    [Property]
    internal void ToOption_int(int i)
    {
        AssertToOption<int>(i);
        AssertToOption<int>(null);
    }

    [Property]
    internal void ToOption_nullableint(int? i)
    {
        AssertToOption(i);
        AssertToOption<int>(null);

    }

    [Property]
    internal void ToOption_decimal(decimal option)
    {
        AssertToOption<decimal>(option);
        AssertToOption<decimal>(null);
    }

    [Property]
    internal void ToOption_nullabledecimal(decimal? option)
    {
        AssertToOption<decimal>(option);
        AssertToOption<decimal>(null);
    }

    [Property]
    internal void ToOption_double(double option)
    {
        AssertToOption<double>(option);
        AssertToOption<double>(null);
    }

    [Property]
    internal void ToOption_nullabledouble(double? option)
    {
        AssertToOption<double>(option);
        AssertToOption<double>(null);
    }

    [Property]
    internal void ToOption_bool(bool option)
    {
        AssertToOption<bool>(option);
        AssertToOption<bool>(null);
    }

    [Property]
    internal void ToOption_nullablebool(bool? option)
    {
        AssertToOption<bool>(option);
        AssertToOption<bool>(null);
    }

    [Property]
    internal void ToOption_ReferenceType(ReferenceType option)
    {
        AssertToOption<ReferenceType>(option);
        AssertToOption<ReferenceType>(null);
    }

    private void AssertToOption<T>(T? value)
        where T : class
    {
        var option = value.ToOption();
        Assert.Equal(value is null, option.IsEmpty);

        if(value is not null)
        {
            Assert.Equal(value, option.Get());
        }
    }

    private void AssertToOption<T>(T? value)
        where T : struct
    {
        var option = value.ToOption();
        Assert.Equal(value is null, option.IsEmpty);

        if(value is not null)
        {
            Assert.Equal(value, option.Get());
        }
    }
}