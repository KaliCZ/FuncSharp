using System;
using System.Linq;

namespace FuncSharp;

public struct PositiveDecimal : IEquatable<PositiveDecimal>
{
    public static readonly PositiveDecimal One = new(1m);

    private PositiveDecimal(decimal value)
    {
        Value = value;
    }

    public decimal Value { get; }

    public static implicit operator decimal(PositiveDecimal d)
    {
        return d.Value;
    }

    public static implicit operator NonNegativeDecimal(PositiveDecimal d)
    {
        return NonNegativeDecimal.Create(d.Value);
    }

    public static PositiveDecimal operator +(PositiveDecimal d1, PositiveDecimal d2)
    {
        return new(d1.Value + d2.Value);
    }

    public static PositiveDecimal operator +(PositiveDecimal d1, NonNegativeDecimal d2)
    {
        return new(d1.Value + d2.Value);
    }

    public static PositiveDecimal operator *(PositiveDecimal d1, PositiveDecimal d2)
    {
        return new(d1.Value * d2.Value);
    }

    public static PositiveDecimal Create(decimal value)
    {
        return TryCreate(value) ?? throw new ArgumentException($"'{value}' is not a positive decimal.");
    }

    public static PositiveDecimal? TryCreate(decimal value)
    {
        return value > 0 ? new(value) : null;
    }

    public static PositiveDecimal? TryCreate(decimal? value)
    {
        if (value is null)
        {
            return null;
        }
        return TryCreate(value.Value);
    }

    public PositiveDecimal Sum(params PositiveDecimal[] values)
    {
        return new(values.Aggregate(Value, (d1, d2) => d1 + d2));
    }

    public PositiveDecimal Multiply(params PositiveDecimal[] values)
    {
        return new(values.Aggregate(Value, (d1, d2) => d1 * d2));
    }

    public PositiveDecimal Min(PositiveDecimal other)
    {
        return new(Math.Min(Value, other.Value));
    }

    public override string ToString()
    {
        return Value.ToString();
    }

    public static bool operator ==(PositiveDecimal left, PositiveDecimal right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(PositiveDecimal left, PositiveDecimal right)
    {
        return !left.Equals(right);
    }

    public bool Equals(PositiveDecimal other)
    {
        return Value == other.Value;
    }

    public override bool Equals(object? obj)
    {
        return obj is PositiveDecimal other && Equals(other);
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }
}