using System;
using FsCheck.Xunit;
using Xunit;

namespace FuncSharp.Tests.Numeric;

public class PositiveLongTests
{
    [Fact]
    internal void AsPositive_Manual()
    {
        Assert.Null((-14L).AsPositive());
        Assert.Null((-1L).AsPositive());
        Assert.Null(0L.AsPositive());

        Assert.Equal(1L, 1L.AsPositive()!.Value);
        Assert.Equal(20L, 20L.AsPositive()!.Value);
        Assert.Equal(26579L, 26579L.AsPositive()!.Value);
    }

    [Property]
    internal void AsPositive(long number)
    {
        var result = number.AsPositive();
        if (number > 0)
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
        var firstOption = first.AsPositive();
        var secondOption = second.AsPositive();
        var bothOptionsEmpty = firstOption is null && secondOption is null;
        if (!bothOptionsEmpty)
        {
            Assert.Equal(numbersAreEqual, firstOption == secondOption);
        }
    }
}