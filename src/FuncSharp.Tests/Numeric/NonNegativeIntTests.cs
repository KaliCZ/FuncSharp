using System;
using FsCheck.Xunit;
using Xunit;

namespace FuncSharp.Tests.Numeric;

public class NonNegativeIntTests
{
    [Fact]
    internal void AsNonNegative_Manual()
    {
        Assert.Null((-14).AsNonNegative());
        Assert.Null((-1).AsNonNegative());

        Assert.Equal(0, 0.AsNonNegative()!.Value);
        Assert.Equal(1, 1.AsNonNegative()!.Value);
        Assert.Equal(20, 20.AsNonNegative()!.Value);
        Assert.Equal(26579, 26579.AsNonNegative()!.Value);
    }

    [Property]
    internal void AsNonNegative(int number)
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
    internal void Equality(int first, int second)
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