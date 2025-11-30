using System;
using FsCheck.Xunit;
using Xunit;

namespace FuncSharp.Tests.Numeric;

public class NonNegativeDecimalTests
{
    [Fact]
    internal void AsNonNegative_Manual()
    {
        Assert.Null((-14m).AsNonNegative());
        Assert.Null((-1m).AsNonNegative());

        Assert.Equal(0m, 0m.AsNonNegative()!.Value);
        Assert.Equal(1m, 1m.AsNonNegative()!.Value);
        Assert.Equal(20m, 20m.AsNonNegative()!.Value);
        Assert.Equal(26579m, 26579m.AsNonNegative()!.Value);
    }

    [Property]
    internal void AsNonNegative(decimal number)
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
    internal void Equality(decimal first, decimal second)
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