using System;
using System.Diagnostics.Contracts;
using System.Globalization;

namespace FuncSharp;

public static class StringExtensions
{
    /// <summary>
    /// Returns a type-safe option of NonEmptyString in case the string is not empty nor whitespace.
    /// </summary>
    [Pure]
    public static NonEmptyString? AsNonEmpty(this string s)
    {
        return NonEmptyString.TryCreate(s);
    }

    [Obsolete("This is already a nonempty string", error: true)]
    public static NonEmptyString? AsNonEmpty(this NonEmptyString s)
    {
        return s;
    }

    [Pure]
    public static byte? ToByte(this string s, IFormatProvider? format = null, NumberStyles style = NumberStyles.Integer)
    {
        return byte.TryParse(s, style, format, out var value)
            ? value
            : null;
    }

    [Pure]
    public static short? ToShort(this string s, IFormatProvider? format = null, NumberStyles style = NumberStyles.Integer)
    {
        return short.TryParse(s, style, format, out var value)
            ? value
            : null;
    }

    [Pure]
    public static int? ToInt(this string s, IFormatProvider? format = null, NumberStyles style = NumberStyles.Integer)
    {
        return int.TryParse(s, style, format, out var value)
            ? value
            : null;
    }

    [Pure]
    public static long? ToLong(this string s, IFormatProvider? format = null, NumberStyles style = NumberStyles.Integer)
    {
        return long.TryParse(s, style, format, out var value)
            ? value
            : null;
    }

    [Pure]
    public static float? ToFloat(this string s, IFormatProvider? format = null, NumberStyles style = NumberStyles.Float | NumberStyles.AllowThousands)
    {
        return float.TryParse(s, style, format, out var value)
            ? value
            : null;
    }

    [Pure]
    public static double? ToDouble(this string s, IFormatProvider? format = null, NumberStyles style = NumberStyles.Float | NumberStyles.AllowThousands)
    {
        return double.TryParse(s, style, format, out var value)
            ? value
            : null;
    }

    [Pure]
    public static decimal? ToDecimal(this string s, IFormatProvider? format = null, NumberStyles style = NumberStyles.Number)
    {
        return decimal.TryParse(s, style, format, out var value)
            ? value
            : null;
    }

    [Pure]
    public static bool? ToBool(this string s)
    {
        return bool.TryParse(s, out var value)
            ? value
            : null;
    }

    [Pure]
    public static DateTime? ToDateTime(this string s, IFormatProvider? format = null, DateTimeStyles style = DateTimeStyles.None)
    {
        return DateTime.TryParse(s, format, style, out var value)
            ? value
            : null;
    }

    [Pure]
    public static TimeSpan? ToTimeSpan(this string s, IFormatProvider? format = null)
    {
        return TimeSpan.TryParse(s, format, out var value)
            ? value
            : null;
    }

    [Pure]
    public static Guid? ToGuid(this string s)
    {
        return Guid.TryParse(s, out var value)
            ? value
            : null;
    }

    [Pure]
    public static Guid? ToGuidExact(this string s, string format = "D")
    {
        return Guid.TryParseExact(s, format, out var value)
            ? value
            : null;
    }



    [Pure]
    public static TEnum? ToEnum<TEnum>(this string s, bool ignoreCase = false)
        where TEnum : struct
    {
        if (s.Contains(",") || !Enum.TryParse(s, ignoreCase, out TEnum value))
        {
            return null;
        }

        if (!Enum.IsDefined(typeof(TEnum), value) || !s.Equals(value.ToString(), StringComparison.InvariantCultureIgnoreCase))
        {
            return null;
        }

        return value;
    }
}