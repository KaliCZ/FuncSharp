using System;
using System.Linq;

namespace FuncSharp;

public struct NonPositiveShort : IEquatable<NonPositiveShort>
{
    public static readonly NonPositiveShort Zero = new(0);

    private NonPositiveShort(short value)
    {
        Value = value;
    }

    public short Value { get; }

    public static implicit operator short(NonPositiveShort i)
    {
        return i.Value;
    }

    public static NonPositiveShort operator +(NonPositiveShort a, NonPositiveShort b)
    {
        return a.Sum(b);
    }

    public static NonPositiveShort Create(short value)
    {
        return TryCreate(value) ?? throw new ArgumentException($"'{value}' is not a non-positive short.");
    }

    public static NonPositiveShort? TryCreate(short value)
    {
        return value <= 0 ? new NonPositiveShort(value) : null;
    }

    public static NonPositiveShort? TryCreate(short? value)
    {
        if (value is null)
        {
            return null;
        }
        return TryCreate(value.Value);
    }

    public NonPositiveShort Sum(params NonPositiveShort[] values)
    {
        return new NonPositiveShort(values.Aggregate(Value, (a, b) => (short)(a + b)));
    }

    public NonPositiveShort Min(NonPositiveShort other)
    {
        return new(Math.Min(Value, other.Value));
    }

    public override string ToString()
    {
        return Value.ToString();
    }

    public static bool operator ==(NonPositiveShort left, NonPositiveShort right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(NonPositiveShort left, NonPositiveShort right)
    {
        return !left.Equals(right);
    }

    public bool Equals(NonPositiveShort other)
    {
        return Value == other.Value;
    }

    public override bool Equals(object? obj)
    {
        return obj is NonPositiveShort other && Equals(other);
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }
}