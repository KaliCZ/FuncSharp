using System;

namespace FuncSharp;

public readonly struct Digit : IEquatable<Digit>
{
    private Digit(byte value)
    {
        Value = value;
    }

    public byte Value { get; }
    public static implicit operator byte(Digit i) => i.Value;
    public static implicit operator int(Digit i) => i.Value;

    public static Digit Create(char value)
    {
        return TryCreate(value) ?? throw new ArgumentException($"Value '{value}' is not a digit.");
    }

    public static Digit? TryCreate(char value)
    {
        return char.IsDigit(value)
            ? new Digit(byte.Parse(value.ToString()))
            : null;
    }

    public static Digit? TryCreate(char? value)
    {
        if (value is null)
        {
            return null;
        }
        return TryCreate(value.Value);
    }

    public static bool operator ==(Digit left, Digit right) => left.Equals(right);

    public static bool operator !=(Digit left, Digit right) => !left.Equals(right);

    public override bool Equals(object? obj)
    {
        return obj is Digit other && Equals(other);
    }

    public bool Equals(Digit other)
    {
        return Value == other.Value;
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }

    public override string ToString()
    {
        return Value.ToString();
    }
}
