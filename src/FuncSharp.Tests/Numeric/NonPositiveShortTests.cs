using System;
using FsCheck.Xunit;
using Xunit;

namespace FuncSharp.Tests.Numeric;

public class NonPositiveShortTests
{
    [Fact]
    internal void AsNonPositive_Manual()
    {
        Assert.Null(((short)14).AsNonPositive());
        Assert.Null(((short)1).AsNonPositive());

        Assert.Equal(0, ((short)0).AsNonPositive()!.Value);
        Assert.Equal(-1, ((short)-1).AsNonPositive()!.Value);
        Assert.Equal(-20, ((short)-20).AsNonPositive()!.Value);
        Assert.Equal(-26579, ((short)-26579).AsNonPositive()!.Value);
    }

    [Property]
    internal void AsNonPositive(short number)
    {
        var result = number.AsNonPositive();
        if (number <= 0)
        {
            Assert.NotNull(result);
            Assert.Equal(number, result!.Value);
            Assert.Equal(number, result!.Value.Value);
        }
        else
        {
            Assert.Null(result);
        }
    }
    
    [Property]
    internal void Equality(short first, short second)
    {
        var numbersAreEqual = first == second;
        var firstOption = first.AsNonPositive();
        var secondOption = second.AsNonPositive();
        var bothOptionsEmpty = firstOption is null && secondOption is null;
        if (!bothOptionsEmpty)
        {
            Assert.Equal(numbersAreEqual, firstOption == secondOption);
        }
    }
}