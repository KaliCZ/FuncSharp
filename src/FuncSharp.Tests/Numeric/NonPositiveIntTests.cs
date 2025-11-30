using System;
using FsCheck.Xunit;
using Xunit;

namespace FuncSharp.Tests.Numeric;

public class NonPositiveIntTests
{
    [Fact]
    internal void AsNonPositive_Manual()
    {
        Assert.Null(14.AsNonPositive());
        Assert.Null(1.AsNonPositive());

        Assert.Equal(0, 0.AsNonPositive()!.Value);
        Assert.Equal(-1, (-1).AsNonPositive()!.Value);
        Assert.Equal(-20, (-20).AsNonPositive()!.Value);
        Assert.Equal(-26579, (-26579).AsNonPositive()!.Value);
    }

    [Property]
    internal void AsNonPositive(int number)
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
    internal void Equality(int first, int second)
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