using System;
using FsCheck.Xunit;
using Xunit;

namespace FuncSharp.Tests.Numeric;

public class PositiveIntTests
{
    [Fact]
    internal void AsPositive_Manual()
    {
        Assert.Null((-14).AsPositive());
        Assert.Null((-1).AsPositive());
        Assert.Null(0.AsPositive());

        Assert.Equal(1, 1.AsPositive()!.Value);
        Assert.Equal(20, 20.AsPositive()!.Value);
        Assert.Equal(26579, 26579.AsPositive()!.Value);
    }

    [Property]
    internal void AsPositive(int number)
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
    internal void Equality(int first, int second)
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