using System;
using System.Linq;

namespace FuncSharp;

public struct NonNegativeShort : IEquatable<NonNegativeShort>
{
    public static readonly NonNegativeShort Zero = new(0);

    private NonNegativeShort(short value)
    {
        Value = value;
    }

    public short Value { get; }

    public static implicit operator short(NonNegativeShort i)
    {
        return i.Value;
    }

    public static NonNegativeShort operator +(NonNegativeShort a, NonNegativeShort b)
    {
        return a.Sum(b);
    }

    public static NonNegativeShort Create(short value)
    {
        return TryCreate(value) ?? throw new ArgumentException($"'{value}' is not a non-negative short.");
    }

    public static NonNegativeShort? TryCreate(short value)
    {
        return value >= 0 ? new NonNegativeShort(value) : null;
    }

    public static NonNegativeShort? TryCreate(short? value)
    {
        if (value is null)
        {
            return null;
        }
        return TryCreate(value.Value);
    }

    public NonNegativeShort Sum(params NonNegativeShort[] values)
    {
        return new NonNegativeShort(values.Aggregate(Value, (a, b) => (short)(a + b)));
    }

    public override string ToString()
    {
        return Value.ToString();
    }

    public static bool operator ==(NonNegativeShort left, NonNegativeShort right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(NonNegativeShort left, NonNegativeShort right)
    {
        return !left.Equals(right);
    }

    public bool Equals(NonNegativeShort other)
    {
        return Value == other.Value;
    }

    public override bool Equals(object? obj)
    {
        return obj is NonNegativeShort other && Equals(other);
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }
}