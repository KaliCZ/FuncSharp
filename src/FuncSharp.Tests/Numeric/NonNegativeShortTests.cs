using System;
using FsCheck.Xunit;
using Xunit;

namespace FuncSharp.Tests.Numeric;

public class NonNegativeShortTests
{
    [Fact]
    internal void AsNonNegative_Manual()
    {
        Assert.Null(((short)-14).AsNonNegative());
        Assert.Null(((short)-1).AsNonNegative());

        Assert.Equal(0, ((short)0).AsNonNegative()!.Value);
        Assert.Equal(1, ((short)1).AsNonNegative()!.Value);
        Assert.Equal(20, ((short)20).AsNonNegative()!.Value);
        Assert.Equal(26579, ((short)26579).AsNonNegative()!.Value);
    }

    [Property]
    internal void AsNonNegative(short number)
    {
        var result = number.AsNonNegative();
        if (number >= 0)
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
        var firstOption = first.AsNonNegative();
        var secondOption = second.AsNonNegative();
        var bothOptionsEmpty = firstOption is null && secondOption is null;
        if (!bothOptionsEmpty)
        {
            Assert.Equal(numbersAreEqual, firstOption == secondOption);
        }
    }
}