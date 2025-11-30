using System;
using FsCheck.Xunit;
using Xunit;

namespace FuncSharp.Tests.Numeric;

public class NonPositiveDecimalTests
{
    [Fact]
    internal void AsNonPositive_Manual()
    {
        Assert.Null(14m.AsNonPositive());
        Assert.Null(1m.AsNonPositive());

        Assert.Equal(0m, 0m.AsNonPositive()!.Value);
        Assert.Equal(-1m, (-1m).AsNonPositive()!.Value);
        Assert.Equal(-20m, (-20m).AsNonPositive()!.Value);
        Assert.Equal(-26579m, (-26579m).AsNonPositive()!.Value);
    }

    [Property]
    internal void AsNonPositive(decimal number)
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
    internal void Equality(decimal first, decimal second)
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