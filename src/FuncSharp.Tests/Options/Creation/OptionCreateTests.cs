using FsCheck;
using FsCheck.Xunit;
using FuncSharp.Tests.Generative;
using Xunit;

namespace FuncSharp.Tests.Options;

public class OptionCreateTests
{
    public OptionCreateTests()
    {
        Arb.Register<OptionGenerators>();
    }

    [Fact]
    public void Create()
    {
        OptionAssert.NonEmptyWithValue(2, Option.Create<int>(2));
        OptionAssert.NonEmptyWithValue(new ReferenceType(14), Option.Create(new ReferenceType(14)));
        OptionAssert.NonEmptyWithValue(Unit.Value, Option.Create(Unit.Value));

        OptionAssert.IsEmpty(Option.Create<ReferenceType>(null));
        OptionAssert.IsEmpty(Option.Create<Unit>(null));
    }

    [Property]
    internal void OptionCreate_int(int i)
    {
        AssertCreate<int>(i);
        AssertCreate<int>(null);
    }

    [Property]
    internal void OptionCreate_nullableint(int? i)
    {
        AssertCreate(i);
        AssertCreate<int>(null);

    }

    [Property]
    internal void OptionCreate_decimal(decimal option)
    {
        AssertCreate<decimal>(option);
        AssertCreate<decimal>(null);
    }

    [Property]
    internal void OptionCreate_nullabledecimal(decimal? option)
    {
        AssertCreate<decimal>(option);
        AssertCreate<decimal>(null);
    }

    [Property]
    internal void OptionCreate_double(double option)
    {
        AssertCreate<double>(option);
        AssertCreate<double>(null);
    }

    [Property]
    internal void OptionCreate_nullabledouble(double? option)
    {
        AssertCreate<double>(option);
        AssertCreate<double>(null);
    }

    [Property]
    internal void OptionCreate_bool(bool option)
    {
        AssertCreate<bool>(option);
        AssertCreate<bool>(null);
    }

    [Property]
    internal void OptionCreate_nullablebool(bool? option)
    {
        AssertCreate<bool>(option);
        AssertCreate<bool>(null);
    }

    [Property]
    internal void OptionCreate_ReferenceType(ReferenceType option)
    {
        AssertCreate<ReferenceType>(option);
        AssertCreate<ReferenceType>(null);
    }

    private void AssertCreate<T>(T? value)
        where T : class
    {
        var option = Option.Create(value);
        Assert.Equal(value is null, option.IsEmpty);

        if (value is not null)
        {
            Assert.Equal(value, option.Get());
        }
    }

    private void AssertCreate<T>(T? value)
        where T : struct
    {
        var option = Option.Create(value);
        Assert.Equal(value is null, option.IsEmpty);

        if (value is not null)
        {
            Assert.Equal(value, option.Get());
        }
    }
}