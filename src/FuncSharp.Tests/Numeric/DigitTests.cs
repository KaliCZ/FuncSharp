using System;
using System.Linq;
using FsCheck.Xunit;
using Xunit;

namespace FuncSharp.Tests.Numeric;

public class DigitTests
{
    [Fact]
    internal void AsDigit()
    {
        AssertDigitOption(0, '0'.AsDigit());
        AssertDigitOption(1, '1'.AsDigit());
        AssertDigitOption(2, '2'.AsDigit());
        AssertDigitOption(3, '3'.AsDigit());
        AssertDigitOption(4, '4'.AsDigit());
        AssertDigitOption(5, '5'.AsDigit());
        AssertDigitOption(6, '6'.AsDigit());
        AssertDigitOption(7, '7'.AsDigit());
        AssertDigitOption(8, '8'.AsDigit());
        AssertDigitOption(9, '9'.AsDigit());

        Assert.Null('a'.AsDigit());
        Assert.Null('z'.AsDigit());
        Assert.Null('B'.AsDigit());
        Assert.Null(char.MinValue.AsDigit());
        Assert.Null(char.MaxValue.AsDigit());
    }

    [Fact]
    internal void Equality()
    {
        Assert.Equal('0'.AsDigit(), '0'.AsDigit());
        Assert.Equal('1'.AsDigit(), '1'.AsDigit());
        Assert.Equal('2'.AsDigit(), '2'.AsDigit());
        Assert.Equal('3'.AsDigit(), '3'.AsDigit());
        Assert.Equal('4'.AsDigit(), '4'.AsDigit());

        Assert.NotEqual('0'.AsDigit(), '9'.AsDigit());
        Assert.NotEqual('1'.AsDigit(), '8'.AsDigit());
        Assert.NotEqual('2'.AsDigit(), '7'.AsDigit());
        Assert.NotEqual('3'.AsDigit(), '6'.AsDigit());
        Assert.NotEqual('4'.AsDigit(), '5'.AsDigit());
    }

    [Fact]
    internal void FilterDigits()
    {
        Assert.Equal(
            new byte[] { 1, 2, 3, 8, 7, 6, 5, 9, },
            "ASD 1 some spaces 2 with numbers 38 7 in between .6 ?:`'!@(#*&$%&^!@)$_  them59".FilterDigits().Select(d => d.Value)
        );
    }

    [Property]
    internal void AllNumbersSucceed(int number)
    {
        var firstDigit = Math.Abs(number).ToString()[0];
        Assert.NotNull(firstDigit.AsDigit());
    }

    [Property]
    internal void AllCharsSucceed(char c)
    {
        var result = c.AsDigit();
        Assert.Equal(char.IsDigit(c), result is not null);
    }

    private void AssertDigitOption(byte value, Digit? digit)
    {
        Assert.True(digit is not null, "Option was expected to have a value, but was empty.");
        Assert.Equal(value, digit!.Value);
        Assert.Equal(value, digit!.Value.Value);
    }
}