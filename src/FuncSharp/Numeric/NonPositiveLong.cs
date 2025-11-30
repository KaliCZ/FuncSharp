using System;
using System.Linq;

namespace FuncSharp;

public struct NonPositiveLong : IEquatable<NonPositiveLong>
{
    public static readonly NonPositiveLong Zero = new(0);

    private NonPositiveLong(long value)
    {
        Value = value;
    }

    public long Value { get; }

    public static implicit operator long(NonPositiveLong i)
    {
        return i.Value;
    }

    public static NonPositiveLong operator +(NonPositiveLong a, NonPositiveLong b)
    {
        return a.Sum(b);
    }

    public static NonPositiveLong Create(long value)
    {
        return TryCreate(value) ?? throw new ArgumentException($"'{value}' is not a non-positive long.");
    }

    public static NonPositiveLong? TryCreate(long value)
    {
        return value <= 0 ? new NonPositiveLong(value) : null;
    }

    public static NonPositiveLong? TryCreate(long? value)
    {
        if (value is null)
        {
            return null;
        }
        return TryCreate(value.Value);
    }

    public NonPositiveLong Sum(params NonPositiveLong[] values)
    {
        return new NonPositiveLong(values.Aggregate(Value, (a, b) => a + b));
    }

    public NonPositiveLong Min(NonPositiveLong other)
    {
        return new(Math.Min(Value, other.Value));
    }

    public override string ToString()
    {
        return Value.ToString();
    }

    public static bool operator ==(NonPositiveLong left, NonPositiveLong right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(NonPositiveLong left, NonPositiveLong right)
    {
        return !left.Equals(right);
    }

    public bool Equals(NonPositiveLong other)
    {
        return Value == other.Value;
    }

    public override bool Equals(object? obj)
    {
        return obj is NonPositiveLong other && Equals(other);
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }
}