using System;
using System.Diagnostics.Contracts;
using System.Threading.Tasks;

namespace FuncSharp;

public static partial class OptionExtensions
{
    /// <summary>
    /// Returns value of the option if it has value. If not, returns null.
    /// </summary>
    [Pure]
    public static T? GetOrNull<T>(this Option<T> option)
        where T : notnull
    {
        return option.Value;
    }

    /// <summary>
    /// Returns value of the option if it has value. If not, returns null.
    /// </summary>
    [Pure]
    public static R? GetOrNull<T, R>(this Option<T> option, Func<T, R> func)
        where T : notnull
    {
        if (option.NonEmpty)
            return func(option.Value!);
        return default;
    }

    /// <summary>
    /// Returns value of the option if it has value. If not, returns the <paramref name="otherwise"/>.
    /// </summary>
    [Pure]
    public static B GetOrElse<A, B>(this Option<A> option, B otherwise)
        where A : notnull, B
    {
        if (option.NonEmpty)
        {
            return option.Value!;
        }
        return otherwise;
    }

    /// <summary>
    /// Returns value of the option if it has value. If not, returns value created by the otherwise function.
    /// </summary>
    [Pure]
    public static B GetOrElse<A, B>(this Option<A> option, Func<Unit, B> otherwise)
        where A : notnull, B
    {
        if (option.NonEmpty)
        {
            return option.Value!;
        }
        return otherwise(Unit.Value);
    }

    /// <summary>
    /// Returns the option if it has value. Otherwise returns the alternative option.
    /// </summary>
    [Pure]
    public static Option<B> OrElse<A, B>(this Option<A> option, Option<B> alternative)
        where A : B where B : notnull
    {
        if (option.NonEmpty)
        {
            return option.Map(value => (B)value);
        }
        return alternative;
    }

    /// <summary>
    /// Returns the option if it has value. Otherwise returns the alternative option.
    /// </summary>
    [Pure]
    public static Option<B> OrElse<A, B>(this Option<A> option, Func<Unit, Option<B>> alternative)
        where A : B where B : notnull
    {
        if (option.NonEmpty)
        {
            return option.Map(value => (B)value);
        }
        return alternative(Unit.Value);
    }

    /// <summary>
    /// Maps value of the current option (if present) into a new value using the specified function and
    /// returns a new option with that new value.
    /// </summary>
    [Pure]
    public static Option<B> Select<A, B>(this Option<A> option, Func<A, B> f)
        where A : notnull where B : notnull
    {
        return option.Map(f);
    }

    /// <summary>
    /// Maps value of the current option (if present) into a new option using the specified function and
    /// returns that new option.
    /// </summary>
    [Pure]
    public static Option<B> SelectMany<A, B>(this Option<A> option, Func<A, Option<B>> f)
        where B : notnull where A : notnull
    {
        return option.FlatMap(f);
    }

    /// <summary>
    /// Maps the current value to a new option using the specified function and combines values of both of the options.
    /// </summary>
    [Pure]
    public static Option<B> SelectMany<A, X, B>(this Option<A> option, Func<A, Option<X>> f, Func<A, X, B> compose)
        where B : notnull where A : notnull where X : notnull
    {
        return option.FlatMap(a => f(a).Map(x => compose(a, x)));
    }

    /// <summary>
    /// Retuns the current option only if its value matches the specified predicate. Otherwise returns an empty option.
    /// </summary>
    [Pure]
    public static Option<A> Where<A>(this Option<A> option, Func<A, bool> predicate)
        where A : notnull
    {
        if (option.IsEmpty || !predicate(option.Value!))
        {
            return Option.Empty<A>();
        }
        return option;
    }

    /// <summary>
    /// Retuns true if value of the option matches the specified predicate. Otherwise returns false.
    /// </summary>
    [Pure]
    public static bool Is<A>(this Option<A> option, Func<A, bool> predicate)
        where A : notnull
    {
        if (option.NonEmpty)
            return predicate(option.Value!);
        return false;
    }

    /// <summary>
    /// Turns the option into a try using the exception in case of empty option.
    /// </summary>
    [Pure]
    public static Try<A, E> ToTry<A, E>(this Option<A> option, Func<Unit, E> e)
        where A : notnull
    {
        if (option.NonEmpty)
            return Try.Success<A, E>(option.Value!);

        return Try.Error<A, E>(e(Unit.Value));
    }

    /// <summary>
    /// Maps value of the current <see cref="Option{A}"/> (if present) into a new value using the specified function and
    /// returns a new <see cref="Option{A}"/> (with that new value) wrapped in a <see cref="System.Threading.Tasks.Task"/>.
    /// </summary>
    [Pure]
    public static async Task<Option<B>> MapAsync<A, B>(this Option<A> option, Func<A, Task<B>> f)
        where B : notnull where A : notnull
    {
        if (option.NonEmpty)
        {
            return Option.Valued(await f(option.Value!));
        }
        else
        {
            return Option.Empty<B>();
        }
    }

    [Pure]
    public static async Task MatchAsync<A>(this Option<A> option, Func<A, Task> ifFirst, Func<Unit, Task>? ifSecond = null)
        where A : notnull
    {
        if (option.NonEmpty)
        {
            await ifFirst(option.Value!);
        }
        else
        {
            if (ifSecond != null)
            {
                await ifSecond(Unit.Value);
            }
        }
    }

    [Pure]
    public static async Task<TResult> MatchAsync<A, TResult>(this Option<A> option, Func<A, Task<TResult>> ifFirst, Func<Unit, Task<TResult>> ifSecond)
        where A : notnull
    {
        if (option.NonEmpty)
        {
            return await ifFirst(option.Value!);
        }
        else
        {
            return await ifSecond(Unit.Value);
        }
    }
}