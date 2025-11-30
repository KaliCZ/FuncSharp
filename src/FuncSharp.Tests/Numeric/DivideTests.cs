using Xunit;

namespace FuncSharp.Tests.Numeric;

public class DivideTests
{
    [Fact]
    internal void DivideWithFallback_int()
    {
        Assert.Equal(14.33m, 1.Divide(0, 14.33m));
        Assert.Equal(12.12m, 3489.Divide(0, 12.12m));
    }

    [Fact]
    internal void DivideWithFallback_decimal()
    {
        Assert.Equal(14.33m, 1m.Divide(0, 14.33m));
        Assert.Equal(12.12m, 3489m.Divide(0, 12.12m));
    }

    [Fact]
    internal void Divide_int()
    {
        Assert.Equal(0.5m, 1.Divide(2));
        Assert.Equal(1.5m, 3.Divide(2));
        Assert.Null(1.Divide(0));
        Assert.Null(3489.Divide(0));
    }

    [Fact]
    internal void Divide_decimal()
    {
        Assert.Equal(0.5m, 1m.Divide(2));
        Assert.Equal(1.5m, 3m.Divide(2));
        Assert.Null(1m.Divide(0));
        Assert.Null(3489m.Divide(0));
    }
}