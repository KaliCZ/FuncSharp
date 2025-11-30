using System;
using FsCheck.Xunit;
using Xunit;

namespace FuncSharp.Tests.Numeric;

public class NonPositiveLongTests
{
    [Fact]
    internal void AsNonPositive_Manual()
    {
        Assert.Null(14L.AsNonPositive());
        Assert.Null(1L.AsNonPositive());

        Assert.Equal(0L, 0L.AsNonPositive()!.Value);
        Assert.Equal(-1L, (-1L).AsNonPositive()!.Value);
        Assert.Equal(-20L, (-20L).AsNonPositive()!.Value);
        Assert.Equal(-26579L, (-26579L).AsNonPositive()!.Value);
    }

    [Property]
    internal void AsNonPositive(long number)
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
    internal void Equality(long first, long second)
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