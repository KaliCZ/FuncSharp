using System;
using System.Globalization;
using System.Threading;
using Xunit;

namespace FuncSharp.Tests;

public class StringExtensionsTests
{
    [Fact]
    public void ConversionsWork()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

        Assert.Equal(42, "42".ToByte()!.Value);
        Assert.Equal(42, "42".ToShort()!.Value);
        Assert.Equal(42, "42".ToInt()!.Value);
        Assert.Equal(42, "42".ToLong()!.Value);
        Assert.Equal(0.5f, "0.5".ToFloat()!.Value);
        Assert.Equal(0.5, "0.5".ToDouble()!.Value);
        Assert.Equal(1.234m, "1.234".ToDecimal()!.Value);
        Assert.True("true".ToBool()!.Value);
        Assert.Equal(new DateTime(2000, 1, 1), "1/1/2000".ToDateTime()!.Value);
        Assert.Equal(new TimeSpan(1, 2, 3), "01:02:03".ToTimeSpan()!.Value);
        Assert.Equal(NumberStyles.Integer, "Integer".ToEnum<NumberStyles>()!.Value);
        Assert.Null("Integer,Number".ToEnum<NumberStyles>());
        Assert.Equal((NumberStyles)2, "AllowTrailingWhite".ToEnum<NumberStyles>()!.Value);
        Assert.Null("2".ToEnum<NumberStyles>());
        Assert.Null("999999999".ToEnum<NumberStyles>());
        Assert.Null("c0fb150f6bf344df984a3a0611ae5e4a".ToGuidExact());
    }
}