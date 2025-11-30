using System;
using FsCheck.Xunit;
using Xunit;

namespace FuncSharp.Tests.Numeric;

public class PositiveShortTests
{
    [Fact]
    internal void AsPositive_Manual()
    {
        Assert.Null(((short)-14).AsPositive());
        Assert.Null(((short)-1).AsPositive());
        Assert.Null(((short)0).AsPositive());

        Assert.Equal(1, ((short)1).AsPositive()!.Value);
        Assert.Equal(20, ((short)20).AsPositive()!.Value);
        Assert.Equal(26579, ((short)26579).AsPositive()!.Value);
    }

    [Property]
    internal void AsPositive(short number)
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
    internal void Equality(short first, short second)
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