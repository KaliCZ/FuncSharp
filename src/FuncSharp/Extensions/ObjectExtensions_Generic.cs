using System;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Threading.Tasks;

namespace FuncSharp
{
    public static partial class ObjectExtensions
    {
        [Pure]
        public static INonEmptyEnumerable<T> ToEnumerable<T>(this T value)
        {
            return NonEmptyEnumerable.Create(value);
        }

        /// <summary>
        /// Turns the specified value into an option.
        /// </summary>
        public static Option<T> ToOption<T>(this T? value)
            where T : struct
        {
            return Option.Create(value);
        }

        /// <summary>
        /// Turns the specified value into an option.
        /// </summary>
        public static Option<T> ToOption<T>(this T? value)
            where T : class
        {
            return Option.Create(value);
        }

        /// <summary>
        /// Returns a valued option with the value inside.
        /// </summary>
        public static Option<T> ToValuedOption<T>(this T value)
        {
            return Option.Valued(value);
        }
    }
}