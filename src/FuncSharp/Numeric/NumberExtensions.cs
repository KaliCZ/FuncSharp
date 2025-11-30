namespace FuncSharp;

public static class NumberExtensions
{
    #region Positive numeric types

    public static PositiveShort? AsPositive(this short value)
    {
        return PositiveShort.Create(value);
    }

    public static PositiveInt? AsPositive(this int value)
    {
        return PositiveInt.Create(value);
    }

    public static PositiveLong? AsPositive(this long value)
    {
        return PositiveLong.TryCreate(value);
    }

    public static PositiveDecimal? AsPositive(this decimal value)
    {
        return PositiveDecimal.TryCreate(value);
    }

    #endregion

    #region NonNegative numeric types

    public static NonNegativeShort? AsNonNegative(this short value)
    {
        return NonNegativeShort.Create(value);
    }

    public static NonNegativeInt? AsNonNegative(this int value)
    {
        return NonNegativeInt.Create(value);
    }

    public static NonNegativeLong? AsNonNegative(this long value)
    {
        return NonNegativeLong.Create(value);
    }

    public static NonNegativeDecimal? AsNonNegative(this decimal value)
    {
        return NonNegativeDecimal.Create(value);
    }

    #endregion

    #region NonPositive numeric types

    public static NonPositiveShort? AsNonPositive(this short value)
    {
        return NonPositiveShort.Create(value);
    }

    public static NonPositiveInt? AsNonPositive(this int value)
    {
        return NonPositiveInt.Create(value);
    }

    public static NonPositiveLong? AsNonPositive(this long value)
    {
        return NonPositiveLong.Create(value);
    }

    public static NonPositiveDecimal? AsNonPositive(this decimal value)
    {
        return NonPositiveDecimal.TryCreate(value);
    }

    #endregion

    public static decimal Divide(this int a, decimal b, decimal otherwise)
    {
        return b == 0
            ? otherwise
            : a / b;
    }

    public static decimal Divide(this decimal a, decimal b, decimal otherwise)
    {
        return b == 0
            ? otherwise
            : a / b;
    }

    public static decimal? Divide(this int a, decimal b)
    {
        return b == 0
            ? null
            : a / b;
    }

    public static decimal? Divide(this decimal a, decimal b)
    {
        return b == 0
            ? null
            : a / b;
    }
}