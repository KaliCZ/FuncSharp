using System;
using System.Linq;

namespace FuncSharp;

public struct NonNegativeInt : IEquatable<NonNegativeInt>
{
    public static readonly NonNegativeInt Zero = new(0);

    private NonNegativeInt(int value)
    {
        Value = value;
    }

    public int Value { get; }

    public static implicit operator int(NonNegativeInt i)
    {
        return i.Value;
    }

    public static NonNegativeInt operator +(NonNegativeInt a, NonNegativeInt b)
    {
        return a.Sum(b);
    }

    public static NonNegativeInt Create(int value)
    {
        return TryCreate(value) ?? throw new ArgumentException($"'{value}' is not a non-negative integer.");
    }

    public static NonNegativeInt? TryCreate(int value)
    {
        return value >= 0 ? new NonNegativeInt(value) : null;
    }

    public static NonNegativeInt? TryCreate(int? value)
    {
        if (value is null)
        {
            return null;
        }
        return TryCreate(value.Value);
    }

    public NonNegativeInt Sum(params NonNegativeInt[] values)
    {
        return new NonNegativeInt(values.Aggregate(Value, (a, b) => a + b));
    }

    public override string ToString()
    {
        return Value.ToString();
    }

    public static bool operator ==(NonNegativeInt left, NonNegativeInt right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(NonNegativeInt left, NonNegativeInt right)
    {
        return !left.Equals(right);
    }

    public bool Equals(NonNegativeInt other)
    {
        return Value == other.Value;
    }

    public override bool Equals(object? obj)
    {
        return obj is NonNegativeInt other && Equals(other);
    }

    public override int GetHashCode()
    {
        return Value;
    }
}