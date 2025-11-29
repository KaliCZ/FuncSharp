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
    public static Option<A> Create<A>(A? value)
        where A : class
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
    {
        return new Option<A>(value);
    }

    /// <summary>
    /// Returns an empty option.
    /// </summary>
    [Pure]
    public static Option<A> Empty<A>()
    {
        return Option<A>.Empty;
    }
}

public readonly struct Option<T> : IEquatable<Option<T>>
{
    public Option(T value)
    {
        Value = value;
        NonEmpty = true;
    }

    public Option()
    {
        Value = default;
        NonEmpty = false;
    }

    public static readonly IReadOnlyList<T> EmptyList = new List<T>().AsReadOnly();

    public static Option<T> Empty { get; } = new Option<T>();

    internal T? Value { get; }

    [Pure]
    public bool NonEmpty { get; }

    [Pure]
    public bool IsEmpty => !NonEmpty;

    [Pure]
    public R Match<R>(Func<T, R> ifNonEmpty, Func<Unit, R> ifEmpty)
    {
        if (NonEmpty)
        {
            return ifNonEmpty(Value!);
        }
        return ifEmpty(Unit.Value);
    }

    public void Match(Action<T>? ifNonEmpty = null, Action<Unit>? ifEmpty = null)
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
    public R Get<R>(Func<T, R> func, Func<Unit, Exception>? otherwise = null)
    {
        if (NonEmpty)
        {
            return func(Value!);
        }
        if (otherwise != null)
        {
            throw otherwise(Unit.Value);
        }
        throw new InvalidOperationException($"An empty option does not have a value of type '{typeof(T).Name}'");
    }

    [Pure]
    public T Get(Func<Unit, Exception>? otherwise = null)
    {
        if (NonEmpty)
        {
            return Value!;
        }
        if (otherwise != null)
        {
            throw otherwise(Unit.Value);
        }
        throw new InvalidOperationException($"An empty option does not have a value of type '{typeof(T).Name}'");
    }

    [Pure]
    public Option<B> Map<B>(Func<T, B> f)
    {
        if (NonEmpty)
        {
            return new Option<B>(f(Value!));
        }
        return Option<B>.Empty;
    }

    [Pure]
    public Option<B> MapEmpty<B>(Func<Unit, B> f)
    {
        if (NonEmpty)
        {
            return Option<B>.Empty;
        }
        return new Option<B>(f(Unit.Value));
    }

    [Pure]
    public Option<B> FlatMapEmpty<B>(Func<Unit, Option<B>> f)
    {
        if (NonEmpty)
        {
            return Option<B>.Empty;
        }
        return f(Unit.Value);
    }

    [Pure]
    public Option<B> FlatMap<B>(Func<T, Option<B>> f)
    {
        if (NonEmpty)
        {
            return f(Value!);
        }
        return Option<B>.Empty;
    }

    [Pure]
    public Option<B> FlatMap<B>(Func<T, B?> f)
        where B : class
    {
        if (NonEmpty && f(Value!) is {} result)
        {
            return Option.Valued(result);
        }
        return Option<B>.Empty;
    }

    [Pure]
    public Option<B> FlatMap<B>(Func<T, B?> f)
        where B : struct
    {
        if (NonEmpty && f(Value!) is {} result)
        {
            return Option.Valued(result);
        }
        return Option<B>.Empty;
    }

    /// <summary>
    /// Returns value of the option if it has value. If not, returns null.
    /// </summary>
    [Pure]
    public T? GetOrNull()
    {
        return Value;
    }

    /// <summary>
    /// Returns value of the option if it has value. If not, returns null.
    /// </summary>
    [Pure]
    public R? GetOrNull<R>(Func<T, R> func)
    {
        if (NonEmpty)
            return func(Value!);
        return default;
    }

    /// <summary>
    /// Maps value of the current option (if present) into a new value using the specified function and
    /// returns a new option with that new value.
    /// </summary>
    [Pure]
    public Option<B> Select<B>(Func<T, B> f)
    {
        return Map(f);
    }

    /// <summary>
    /// Maps value of the current option (if present) into a new option using the specified function and
    /// returns that new option.
    /// </summary>
    [Pure]
    public Option<B> SelectMany<B>(Func<T, Option<B>> f)
    {
        return FlatMap(f);
    }

    /// <summary>
    /// Maps the current value to a new option using the specified function and combines values of both of the options.
    /// </summary>
    [Pure]
    public Option<B> SelectMany<X, B>(Func<T, Option<X>> f, Func<T, X, B> compose)
    {
        return FlatMap(a => f(a).Map(x => compose(a, x)));
    }

    /// <summary>
    /// Retuns the current option only if its value matches the specified predicate. Otherwise returns an empty option.
    /// </summary>
    [Pure]
    public Option<T> Where(Func<T, bool> predicate)
    {
        if (IsEmpty || !predicate(Value!))
        {
            return Option.Empty<T>();
        }
        return this;
    }

    /// <summary>
    /// Retuns true if value of the option matches the specified predicate. Otherwise returns false.
    /// </summary>
    [Pure]
    public bool Is(Func<T, bool> predicate)
    {
        if (NonEmpty)
            return predicate(Value!);
        return false;
    }

    /// <summary>
    /// Turns the option into a try using the exception in case of empty option.
    /// </summary>
    [Pure]
    public Try<T, E> ToTry<E>(Func<Unit, E> e)
    {
        if (NonEmpty)
            return Try.Success<T, E>(Value!);

        return Try.Error<T, E>(e(Unit.Value));
    }

    /// <summary>
    /// Maps value of the current <see cref="Option{A}"/> (if present) into a new value using the specified function and
    /// returns a new <see cref="Option{A}"/> (with that new value) wrapped in a <see cref="System.Threading.Tasks.Task"/>.
    /// </summary>
    [Pure]
    public async Task<Option<B>> MapAsync<B>(Func<T, Task<B>> f)
    {
        if (NonEmpty)
        {
            return Option.Valued(await f(Value!));
        }
        else
        {
            return Option.Empty<B>();
        }
    }

    /// <summary>
    /// Maps value of the current <see cref="Option{A}"/> (if present) into a new option using the specified function and
    /// returns <see cref="Option{B}"/> wrapped in a <see cref="System.Threading.Tasks.Task"/>.
    /// </summary>
    [Pure]
    public async Task<Option<B>> FlatMapAsync<B>(Func<T, Task<Option<B>>> f)
    {
        if (NonEmpty)
        {
            return await f(Value!);
        }

        return Option.Empty<B>();
    }

    /// <summary>
    /// Maps value of the current <see cref="Option{A}"/> (if present) into a new option using the specified function and
    /// returns <see cref="Option{B}"/> wrapped in a <see cref="System.Threading.Tasks.Task"/>.
    /// </summary>
    [Pure]
    public async Task<Option<B>> FlatMapAsync<B>(Func<T, Task<B?>> f)
    {
        if (NonEmpty)
        {
            return (await f(Value!)).ToOption();
        }

        return Option.Empty<B>();
    }

    /// <summary>
    /// For empty option, returns an empty list.<br/>
    /// For non-empty option, returns a single-item list with the option value.
    /// </summary>
    [Pure]
    public IReadOnlyList<T> AsReadOnlyList()
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
    public static bool operator ==(Option<T> option1, Option<T> option2)
    {
        return option1.Equals(option2);
    }

    [Pure]
    public static bool operator !=(Option<T> option1, Option<T> option2)
    {
        return !option1.Equals(option2);
    }

    [Pure]
    public bool Equals(Option<T> other)
    {
        return NonEmpty == other.NonEmpty && EqualityComparer<T>.Default.Equals(Value, other.Value);
    }

    [Pure]
    public override bool Equals(object? obj)
    {
        if (obj is Option<T> other)
        {
            return Equals(other);
        }
        return false;
    }
}