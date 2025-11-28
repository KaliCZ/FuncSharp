using System;
using System.Diagnostics.Contracts;
using System.Threading.Tasks;

namespace FuncSharp;

public static partial class OptionExtensions
{
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
}