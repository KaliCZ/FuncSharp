using System;
using FsCheck.Xunit;
using Xunit;

namespace FuncSharp.Tests.Numeric;

public class NonNegativeLongTests
{
    [Fact]
    internal void AsNonNegative_Manual()
    {
        Assert.Null((-14L).AsNonNegative());
        Assert.Null((-1L).AsNonNegative());

        Assert.Equal(0L, 0L.AsNonNegative()!.Value);
        Assert.Equal(1L, 1L.AsNonNegative()!.Value);
        Assert.Equal(20L, 20L.AsNonNegative()!.Value);
        Assert.Equal(26579L, 26579L.AsNonNegative()!.Value);
    }

    [Property]
    internal void AsNonNegative(long number)
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
    internal void Equality(long first, long second)
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