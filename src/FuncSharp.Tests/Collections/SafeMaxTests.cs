using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace FuncSharp.Tests.Collections;

public class SafeMaxTests
{
    [Fact]
    public void SafeMax()
    {
        IEnumerable<string> empty = [];
        IEnumerable<string> nonEmpty = ["a", "abc", "ab"];
        Assert.Null(empty.SafeMax(item => item.Length));
        Assert.Equal(3, nonEmpty.SafeMax(item => item.Length));
    }

    [Fact]
    public void Max()
    {
        IEnumerable<string> empty = [];
        IEnumerable<string> nonEmpty = ["a", "abc", "ab"];
        Assert.Throws<InvalidOperationException>(() => empty.Max(item => item.Length));
        Assert.Equal(3, nonEmpty.Max(item => item.Length));
    }
}