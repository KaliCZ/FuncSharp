using System;
using System.Globalization;
using Xunit;

namespace FuncSharp.Tests.Strings;

public class StringExtensionTests
{
    [Fact]
    public void ToGuid()
    {
        var validGuid = Guid.NewGuid();
        Assert.Equal(validGuid, validGuid.ToString().ToGuid());

        Assert.Null(string.Empty.ToGuid());
        Assert.Null("ASDF".ToGuid());
        Assert.Null($"{validGuid}-{validGuid}".ToGuid());
        Assert.Null(validGuid.ToString().Substring(1).ToGuid());
    }

    [Fact]
    public void ToBool()
    {
        Assert.Equal(true, "true".ToBool());
        Assert.Equal(false, "false".ToBool());

        Assert.Null(string.Empty.ToBool());
        Assert.Null("ASDF".ToBool());
    }

    [Fact]
    public void ToByte()
    {
        Assert.Equal((byte)12, "12".ToByte());
        Assert.Equal((byte)254, "254".ToByte());

        Assert.Null(string.Empty.ToByte());
        Assert.Null("ASDF".ToByte());
        Assert.Null("258".ToByte());// Too big
    }

    [Fact]
    public void ToShort()
    {
        Assert.Equal((short)12, "12".ToShort());
        Assert.Equal((short)32767, "32767".ToShort());

        Assert.Null(string.Empty.ToShort());
        Assert.Null("ASDF".ToShort());
        Assert.Null("32768".ToShort());// Too big
    }

    [Fact]
    public void ToInt()
    {
        Assert.Equal(12, "12".ToInt());
        Assert.Equal(2147483647, "2147483647".ToInt());

        Assert.Null(string.Empty.ToInt());
        Assert.Null("ASDF".ToInt());
        Assert.Null("2,147,483,648".ToInt());// Too big
    }

    [Fact]
    public void ToLong()
    {
        Assert.Equal(12, "12".ToLong());
        Assert.Equal(9223372036854775806, "9223372036854775806".ToLong());

        Assert.Null(string.Empty.ToLong());
        Assert.Null("ASDF".ToLong());
        Assert.Null("9223372036854775808".ToLong());// Too big
    }

    [Fact]
    public void ToFloat()
    {
        Assert.Equal(12f, "12".ToFloat());
        Assert.NotNull("12.628".ToFloat(new NumberFormatInfo()));
        Assert.Equal(12.628f, "12.628".ToFloat(new NumberFormatInfo())!.Value, tolerance: 0.00005f);
        Assert.NotNull("340282300000000000000000000000000000000".ToFloat());
        Assert.Equal(340282200000000000000000000000000000000f, "340282300000000000000000000000000000000".ToFloat()!.Value, tolerance: 1000000000000000000000000000000000f);

        Assert.Null(string.Empty.ToFloat());
        Assert.Null("ASDF".ToFloat());
        Assert.Equal(float.PositiveInfinity, "460282300000000000000000000000000000000".ToFloat()); // It's a value of infinity

    }

    [Fact]
    public void ToDouble()
    {
        Assert.Equal(12d, "12".ToDouble());
        Assert.NotNull("12.628".ToDouble(new NumberFormatInfo()));
        Assert.Equal(12.628d, "12.628".ToDouble(new NumberFormatInfo())!.Value, tolerance: 0.00005);

        Assert.Null(string.Empty.ToDouble());
        Assert.Null("ASDF".ToDouble());
    }

    [Fact]
    public void ToDecimal()
    {
        Assert.Equal(12m, "12".ToDecimal());
        Assert.Equal(12.628m, "12.628".ToDecimal(new NumberFormatInfo()));
        Assert.Equal(79228162514264337593543950335m, "79228162514264337593543950335".ToDecimal());

        Assert.Null(string.Empty.ToDecimal());
        Assert.Null("ASDF".ToDecimal());
        Assert.Null("79228162514264337593543950337".ToDecimal());// Too big
    }

    [Fact]
    public void ToDateTime()
    {
        Assert.Equal(new DateTime(2022,01,13, 16, 25, 35), "2022-01-13T16:25:35".ToDateTime());

        Assert.Null(string.Empty.ToDateTime());
        Assert.Null("ASDF".ToDateTime());
    }

    [Fact]
    public void ToTimeSpan()
    {
        Assert.Equal(new TimeSpan(days: 1, hours: 12, minutes: 24, seconds: 02), "1.12:24:02".ToTimeSpan());

        Assert.Null(string.Empty.ToTimeSpan());
        Assert.Null("ASDF".ToTimeSpan());
    }

    [Fact]
    public void ToEnum()
    {
        Assert.Equal(ParseTestEnum.FirstValue, "FirstValue".ToEnum<ParseTestEnum>());
        Assert.Equal(ParseTestEnum.SecondValue, "SecondValue".ToEnum<ParseTestEnum>());

        Assert.Null(string.Empty.ToEnum<ParseTestEnum>());
        Assert.Null("ASDF".ToEnum<ParseTestEnum>());
    }

    private enum ParseTestEnum
    {
        FirstValue,
        SecondValue
    }
}