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
    public static TResult? Map<T, TResult>(this T? value, Func<T, TResult> func)
        where T : struct
    {
        if (value is {} v)
        {
            return func(v);
        }

        return default;
    }

    /// <summary>
    /// Maps the not null value using the specified function and returns the result.
    /// </summary>
    [Pure]
    public static TResult? Map<T, TResult>(this T? value, Func<T, TResult> func)
        where T : class
    {
        if (value is {} v)
        {
            return func(v);
        }

        return default;
    }

    /// <summary>
    /// Maps the not null value using the specified function and returns the result.
    /// </summary>
    [Pure]
    public static async Task<TResult?> MapAsync<T, TResult>(this T? value, Func<T, Task<TResult>> func)
        where T : struct
    {
        if (value is {} v)
        {
            return await func(v);
        }

        return default;
    }

    /// <summary>
    /// Maps the not null value using the specified function and returns the result.
    /// </summary>
    [Pure]
    public static async Task<TResult?> MapAsync<T, TResult>(this T? value, Func<T, Task<TResult>> func)
        where T : class
    {
        if (value is {} v)
        {
            return await func(v);
        }

        return default;
    }

    /// <summary>
    /// Returns the value only if it matches the specified predicate. Otherwise returns null.
    /// </summary>
    [Pure]
    public static T? Where<T>(this T value, Func<T, bool> predicate)
        where T : notnull
    {
        if (value is {} v && predicate(v))
        {
            return v;
        }
        return default;
    }
}