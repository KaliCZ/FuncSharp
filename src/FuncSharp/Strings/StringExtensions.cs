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
    public static byte? ToByte(this NonEmptyString s, IFormatProvider? format = null, NumberStyles style = NumberStyles.Integer)
    {
        byte.TryParse(s.Value, style, format, out var value);
        return value;
    }

    [Pure]
    public static short? ToShort(this NonEmptyString s, IFormatProvider? format = null, NumberStyles style = NumberStyles.Integer)
    {
        short.TryParse(s.Value, style, format, out var value);
        return value;
    }

    [Pure]
    public static int? ToInt(this NonEmptyString s, IFormatProvider? format = null, NumberStyles style = NumberStyles.Integer)
    {
        int.TryParse(s.Value, style, format, out var value);
        return value;
    }

    [Pure]
    public static long? ToLong(this NonEmptyString s, IFormatProvider? format = null, NumberStyles style = NumberStyles.Integer)
    {
        long.TryParse(s.Value, style, format, out var value);
        return value;
    }

    [Pure]
    public static float? ToFloat(this NonEmptyString s, IFormatProvider? format = null, NumberStyles style = NumberStyles.Float | NumberStyles.AllowThousands)
    {
        float.TryParse(s.Value, style, format, out var value);
        return value;
    }

    [Pure]
    public static double ToDouble(this NonEmptyString s, IFormatProvider? format = null, NumberStyles style = NumberStyles.Float | NumberStyles.AllowThousands)
    {
        double.TryParse(s.Value, style, format, out var value);
        return value;
    }

    [Pure]
    public static decimal? ToDecimal(this NonEmptyString s, IFormatProvider? format = null, NumberStyles style = NumberStyles.Number)
    {
        decimal.TryParse(s.Value, style, format, out var value);
        return value;
    }

    [Pure]
    public static bool? ToBool(this NonEmptyString s)
    {
        bool.TryParse(s.Value, out var value);
        return value;
    }

    [Pure]
    public static DateTime? ToDateTime(this NonEmptyString s, IFormatProvider? format = null, DateTimeStyles style = DateTimeStyles.None)
    {
        DateTime.TryParse(s.Value, format, style, out var value);
        return value;
    }

    [Pure]
    public static TimeSpan? ToTimeSpan(this NonEmptyString s, IFormatProvider? format = null)
    {
        TimeSpan.TryParse(s.Value, format, out var value);
        return value;
    }

    [Pure]
    public static TEnum? ToEnum<TEnum>(this NonEmptyString s, bool ignoreCase = false)
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

    [Pure]
    public static Guid? ToGuid(this NonEmptyString s)
    {
        Guid.TryParse(s.Value, out var value);
        return value;
    }

    [Pure]
    public static Guid? ToGuidExact(this NonEmptyString s, string format = "D")
    {
        Guid.TryParseExact(s, format, out var value);
        return value;
    }
}