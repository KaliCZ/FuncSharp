using System.Linq;
using Xunit;

namespace FuncSharp.Tests;

public class DictionaryTests
{
    [Fact]
    public void GetOrElseSet()
    {
        var dictionary = Enumerable.Range(0, 1000).ToDictionary(i => i, i => $"{i} potatoes");

        OptionAssert.NonEmptyWithValue("0 potatoes", dictionary.TryGet(0));
        OptionAssert.NonEmptyWithValue("14 potatoes", dictionary.TryGet(14));
        OptionAssert.NonEmptyWithValue("128 potatoes", dictionary.TryGet(128));
        OptionAssert.NonEmptyWithValue("999 potatoes", dictionary.TryGet(999));

        OptionAssert.IsEmpty(dictionary.TryGet(-14));
        OptionAssert.IsEmpty(dictionary.TryGet(1000));
        OptionAssert.IsEmpty(dictionary.TryGet(123561));
        OptionAssert.IsEmpty(dictionary.TryGet(-156859615));
    }
}