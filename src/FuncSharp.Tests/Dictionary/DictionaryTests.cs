using System.Linq;
using Xunit;

namespace FuncSharp.Tests;

public class DictionaryTests
{
    [Fact]
    public void GetOrElseSet()
    {
        var dictionary = Enumerable.Range(0, 1000).ToDictionary(i => i, i => $"{i} potatoes");

        Assert.Equal("0 potatoes", dictionary.TryGet(0));
        Assert.Equal("14 potatoes", dictionary.TryGet(14));
        Assert.Equal("128 potatoes", dictionary.TryGet(128));
        Assert.Equal("999 potatoes", dictionary.TryGet(999));

        Assert.Null(dictionary.TryGet(-14));
        Assert.Null(dictionary.TryGet(1000));
        Assert.Null(dictionary.TryGet(123561));
        Assert.Null(dictionary.TryGet(-156859615));
    }
}