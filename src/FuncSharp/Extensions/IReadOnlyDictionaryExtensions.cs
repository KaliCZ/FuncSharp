using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace FuncSharp;

public static class IReadOnlyDictionaryExtensions
{
    [Pure]
    public static TValue? TryGet<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
    {
        dictionary.TryGetValue(key, out var value);
        return value;
    }

    [Pure]
    public static TValue? ASDF<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> dictionary, TKey key)
    {
        IDictionary<string, int> dict1 = new Dictionary<string, int>();
        var dict2 = new Dictionary<string, int?>();
        var dict3 = new Dictionary<string, string>();
        var dict4 = new Dictionary<string, string?>();

        int x1 = dict1.TryGet("");
        int? x2 = dict2.TryGet("");
        string? x3 = dict3.TryGet("");
        string? x4 = dict4.TryGet("");

        dictionary.TryGetValue(key, out TValue? value);
        return value;
    }
}