using System;
using System.Diagnostics.Contracts;
using System.Threading.Tasks;

namespace FuncSharp;

public static partial class OptionExtensions
{
    /// <summary>
    /// Maps the not null value using the specified function and returns the result.
    /// </summary>
    [Pure]
    public static TResult? Map<T, TResult>(this T? value, Func<T, TResult?> func)
        where TResult : struct
    {
        return value is { } v ? func(v) : null;
    }

    /// <summary>
    /// Maps the not null value using the specified function and returns the result.
    /// </summary>
    [Pure]
    public static TResult? Map<T, TResult>(this T? value, Func<T, TResult?> func)
        where TResult : class
    {
        return value is { } v ? func(v) : null;
    }

    /// <summary>
    /// Maps the not null value using the specified function and returns the result.
    /// </summary>
    [Pure]
    public static async Task<TResult?> MapAsync<T, TResult>(this T? value, Func<T, Task<TResult>> func)
        where T : struct
    {
        return value is { } v ? await func(v) : default;
    }

    /// <summary>
    /// Maps the not null value using the specified function and returns the result.
    /// </summary>
    [Pure]
    public static async Task<TResult?> MapAsync<T, TResult>(this T? value, Func<T, Task<TResult>> func)
        where T : class
    {
        return value is { } v ? await func(v) : default;
    }

    /// <summary>
    /// Maps the not null value using the specified function and returns the result.
    /// </summary>
    [Pure]
    public static async Task<TResult?> ConditionalMapAsync<T, TResult>(this T value, Func<T, bool> condition, Func<T, Task<TResult>> func)
        where T : struct
    {
        return condition(value) ? await func(value) : default;
    }
}