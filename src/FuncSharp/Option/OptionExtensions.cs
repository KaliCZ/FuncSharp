using System;
using System.Diagnostics.Contracts;
using System.Threading.Tasks;

namespace FuncSharp;

public static partial class OptionExtensions
{
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
    /// Returns the value of the outer option or an empty opion.
    /// </summary>
    [Pure]
    public static Option<A> Flatten<A>(this Option<Option<A>> option)
    {
        return option.FlatMap(o => o);
    }

    /// <summary>
    /// Turns the option of nullable into an option.
    /// </summary>
    [Pure]
    public static Option<A> Flatten<A>(this Option<A?> option)
        where A : struct
    {
        return option.FlatMap(a => a.ToOption());
    }
}