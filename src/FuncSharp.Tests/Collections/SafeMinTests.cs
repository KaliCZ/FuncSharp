using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace FuncSharp.Tests.Collections;

public class SafeMinTests
{
    [Fact]
    public void SafeMin()
    {
        IEnumerable<string> empty = [];
        IEnumerable<string> nonEmpty = ["a", "abc", "ab"];
        Assert.Null(empty.SafeMin(item => item.Length));
        Assert.Equal(1, nonEmpty.SafeMin(item => item.Length));
    }

    [Fact]
    public void Min()
    {
        IEnumerable<string> empty = [];
        IEnumerable<string> nonEmpty = ["a", "abc", "ab"];
        Assert.Throws<InvalidOperationException>(() => empty.Min(item => item.Length));
        Assert.Equal(1, nonEmpty.Min(item => item.Length));
    }
}