using System;
using FsCheck.Xunit;
using Xunit;

namespace FuncSharp.Tests.Numeric;

public class PositiveDecimalTests
{
    [Fact]
    internal void AsPositive_Manual()
    {
        Assert.Null((-14m).AsPositive());
        Assert.Null((-1m).AsPositive());
        Assert.Null(0m.AsPositive());

        Assert.Equal(1m, 1m.AsPositive()!.Value);
        Assert.Equal(20m, 20m.AsPositive()!.Value);
        Assert.Equal(26579m, 26579m.AsPositive()!.Value);
    }

    [Property]
    internal void AsPositive(decimal number)
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
    internal void Equality(decimal first, decimal second)
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