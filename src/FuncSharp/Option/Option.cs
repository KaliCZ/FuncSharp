using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace FuncSharp;

public static class Option
{
    /// <summary>
    /// True value as an option.
    /// </summary>
    [Pure]
    public static Option<bool> True { get; } = true.ToOption();

    /// <summary>
    /// False value as an option.
    /// </summary>
    [Pure]
    public static Option<bool> False { get; } = false.ToOption();

    /// <summary>
    /// Unit value as an option.
    /// </summary>
    [Pure]
    public static Option<Unit> Unit { get; } = FuncSharp.Unit.Value.ToOption();

    /// <summary>
    /// Creates a new option based on the specified value. Returns option with the value if is is non-null, empty otherwise.
    /// </summary>
    [Pure]
    public static Option<A> Create<A>(A value)
        where A : notnull
    {
        if (value is not null)
        {
            return new Option<A>(value);
        }
        return Option<A>.Empty;
    }

    /// <summary>
    /// Creates a new option based on the specified value. Returns option with the value if is is non-null, empty otherwise.
    /// </summary>
    [Pure]
    public static Option<A> Create<A>(A? value)
        where A : struct
    {
        if (value.HasValue)
        {
            return new Option<A>(value.Value);
        }
        return Option<A>.Empty;
    }

    /// <summary>
    /// Returns an option with the specified value.
    /// </summary>
    [Pure]
    public static Option<A> Valued<A>(A value)
        where A : notnull
    {
        return new Option<A>(value);
    }

    /// <summary>
    /// Returns an empty option.
    /// </summary>
    [Pure]
    public static Option<A> Empty<A>()
        where A : notnull
    {
        return Option<A>.Empty;
    }
}

public struct Option<A> : IEquatable<Option<A>>
    where A : notnull
{
    public Option(A value)
    {
        Value = value;
        NonEmpty = true;
    }

    public Option()
    {
        Value = default;
        NonEmpty = false;
    }

    public static readonly IReadOnlyList<A> EmptyList = new List<A>().AsReadOnly();

    public static Option<A> Empty { get; } = new Option<A>();

    internal A? Value { get; }

    [Pure]
    public bool NonEmpty { get; }

    [Pure]
    public bool IsEmpty => !NonEmpty;

    [Pure]
    public R Match<R>(Func<A, R> ifNonEmpty, Func<Unit, R> ifEmpty)
    {
        if (NonEmpty)
        {
            return ifNonEmpty(Value!);
        }
        return ifEmpty(Unit.Value);
    }

    [Pure]
    public void Match(Action<A>? ifNonEmpty = null, Action<Unit>? ifEmpty = null)
    {
        if (NonEmpty)
        {
            ifNonEmpty?.Invoke(Value!);
        }
        else
        {
            ifEmpty?.Invoke(Unit.Value);
        }
    }

    [Pure]
    public R Get<R>(Func<A, R> func, Func<Unit, Exception>? otherwise = null)
    {
        if (NonEmpty)
        {
            return func(Value!);
        }
        if (otherwise != null)
        {
            throw otherwise(Unit.Value);
        }
        throw new InvalidOperationException($"An empty option does not have a value of type '{typeof(A).Name}'");
    }

    [Pure]
    public A Get(Func<Unit, Exception>? otherwise = null)
    {
        if (NonEmpty)
        {
            return Value!;
        }
        if (otherwise != null)
        {
            throw otherwise(Unit.Value);
        }
        throw new InvalidOperationException($"An empty option does not have a value of type '{typeof(A).Name}'");
    }

    [Pure]
    public Option<B> Map<B>(Func<A, B> f)
        where B : notnull
    {
        if (NonEmpty)
        {
            return new Option<B>(f(Value!));
        }
        return Option<B>.Empty;
    }

    [Pure]
    public Option<B> MapEmpty<B>(Func<Unit, B> f)
        where B : notnull
    {
        if (NonEmpty)
        {
            return Option<B>.Empty;
        }
        return new Option<B>(f(Unit.Value));
    }

    [Pure]
    public Option<B> FlatMapEmpty<B>(Func<Unit, Option<B>> f)
        where B : notnull
    {
        if (NonEmpty)
        {
            return Option<B>.Empty;
        }
        return f(Unit.Value);
    }

    [Pure]
    public Option<B> FlatMap<B>(Func<A, Option<B>> f)
        where B : notnull
    {
        if (NonEmpty)
        {
            return f(Value!);
        }
        return Option<B>.Empty;
    }

    [Pure]
    public Option<B> FlatMap<B>(Func<A, B?> f)
        where B : class
    {
        if (NonEmpty && f(Value!) is {} result)
        {
            return Option.Valued(result);
        }
        return Option<B>.Empty;
    }

    [Pure]
    public Option<B> FlatMap<B>(Func<A, B?> f)
        where B : struct
    {
        if (NonEmpty && f(Value!) is {} result)
        {
            return Option.Valued(result);
        }
        return Option<B>.Empty;
    }

    /// <summary>
    /// Maps value of the current <see cref="Option{A}"/> (if present) into a new option using the specified function and
    /// returns <see cref="Option{B}"/> wrapped in a <see cref="System.Threading.Tasks.Task"/>.
    /// </summary>
    [Pure]
    public async Task<Option<B>> FlatMapAsync<B>(Func<A, Task<Option<B>>> f)
        where B : notnull
    {
        if (NonEmpty)
        {
            return await f(Value!);
        }

        return Option.Empty<B>();
    }

    /// <summary>
    /// For empty option, returns an empty list.<br/>
    /// For non-empty option, returns a single-item list with the option value.
    /// </summary>
    [Pure]
    public IReadOnlyList<A> AsReadOnlyList()
    {
        return NonEmpty
            ? [Value!]
            : EmptyList;
    }

    [Pure]
    public override string ToString()
    {
        if (NonEmpty)
        {
            return "Value(" + Value + ")";
        }
        return "Empty";
    }

    [Pure]
    public override int GetHashCode()
    {
        return HashCode.Combine(Value, NonEmpty);
    }

    [Pure]
    public static bool operator ==(Option<A> option1, Option<A> option2)
    {
        return option1.Equals(option2);
    }

    [Pure]
    public static bool operator !=(Option<A> option1, Option<A> option2)
    {
        return !option1.Equals(option2);
    }

    [Pure]
    public bool Equals(Option<A> other)
    {
        return NonEmpty == other.NonEmpty && EqualityComparer<A>.Default.Equals(Value, other.Value);
    }

    [Pure]
    public override bool Equals(object? obj)
    {
        if (obj is Option<A> other)
        {
            return Equals(other);
        }
        return false;
    }
}