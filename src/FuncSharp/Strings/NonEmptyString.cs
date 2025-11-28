using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace FuncSharp;

public sealed class NonEmptyString : IEquatable<string>, IEquatable<NonEmptyString>
{
    private NonEmptyString(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public int Length => Value.Length;

    public static implicit operator string(NonEmptyString s)
    {
        return s.Value;
    }

    public static explicit operator NonEmptyString(string s)
    {
        return Create(s);
    }

    /// <summary>
    /// Creates a new instance of NonEmptyString or throws an exception if the specified value is null, empty or whitespace.
    /// </summary>
    /// <exception cref="ArgumentException">Exception thrown in case the value is null, empty or whitespace</exception>
    public static NonEmptyString Create(string value)
    {
        return TryCreate(value) ?? throw new ArgumentException("You cannot create NonEmptyString from whitespace, empty string or null.");
    }

    public static NonEmptyString? TryCreate(string value)
    {
        return string.IsNullOrWhiteSpace(value) ? null : new NonEmptyString(value);
    }

    #region Proxy methods to string

    public NonEmptyString ToLower() => Create(Value.ToLower());
    public NonEmptyString ToLower(CultureInfo culture) => Create(Value.ToLower(culture));
    public NonEmptyString ToLowerInvariant() => Create(Value.ToLowerInvariant());

    public NonEmptyString ToUpper() => Create(Value.ToUpper());
    public NonEmptyString ToUpper(CultureInfo culture) => Create(Value.ToUpper(culture));
    public NonEmptyString ToUpperInvariant() => Create(Value.ToUpperInvariant());

    public bool Contains(string s) => Value.Contains(s);
    public bool Contains(string s, StringComparison comparisonType) => Value.Contains(s, comparisonType);
    public bool Contains(char c) => Value.Contains(c);
    public bool Contains(char c, StringComparison comparisonType) => Value.Contains(c, comparisonType);

    public string Replace(char oldChar, char newChar) => Value.Replace(oldChar, newChar);
    public string Replace(string oldString, string newString) => Value.Replace(oldString, newString);
    public string Replace(string oldString, string newString, StringComparison comparisonType) => Value.Replace(oldString, newString, comparisonType);

    public string Trim() => Value.Trim();

    public int IndexOf(string s) => Value.IndexOf(s);
    public int IndexOf(string s, int startIndex) => Value.IndexOf(s, startIndex);
    public int IndexOf(string s, StringComparison comparisonType) => Value.IndexOf(s, comparisonType);
    public int IndexOf(string s, int startIndex, StringComparison comparisonType) => Value.IndexOf(s, startIndex, comparisonType);
    public int IndexOf(char c) => Value.IndexOf(c);
    public int IndexOf(char c, int startIndex) => Value.IndexOf(c, startIndex);
    public int IndexOf(char c, StringComparison comparisonType) => Value.IndexOf(c, comparisonType);

    public int LastIndexOf(string s) => Value.LastIndexOf(s);
    public int LastIndexOf(string s, int startIndex) => Value.LastIndexOf(s, startIndex);
    public int LastIndexOf(string s, StringComparison comparisonType) => Value.LastIndexOf(s, comparisonType);
    public int LastIndexOf(string s, int startIndex, StringComparison comparisonType) => Value.LastIndexOf(s, startIndex, comparisonType);
    public int LastIndexOf(char c) => Value.LastIndexOf(c);
    public int LastIndexOf(char c, int startIndex) => Value.LastIndexOf(c, startIndex);

    public string Substring(int startIndex) => Value.Substring(startIndex);
    public string Substring(int startIndex, int length) => Value.Substring(startIndex, length);

    public bool StartsWith(string s) => Value.StartsWith(s);
    public bool StartsWith(string s, StringComparison comparisonType) => Value.StartsWith(s, comparisonType);
    public bool StartsWith(string s, bool ignoreCase, CultureInfo culture) => Value.StartsWith(s, ignoreCase, culture);
    public bool StartsWith(char c) => Value.StartsWith(c);

    public bool EndsWith(string s) => Value.EndsWith(s);
    public bool EndsWith(string s, StringComparison comparisonType) => Value.EndsWith(s, comparisonType);
    public bool EndsWith(string s, bool ignoreCase, CultureInfo culture) => Value.EndsWith(s, ignoreCase, culture);
    public bool EndsWith(char c) => Value.EndsWith(c);

    #endregion Proxy methods to string

    public override int GetHashCode() => Value.GetHashCode();
    public static bool operator ==(NonEmptyString left, NonEmptyString right) => left.Equals(right);
    public static bool operator !=(NonEmptyString left, NonEmptyString right) => !left.Equals(right);

    public bool Equals(NonEmptyString? other) => Equals(other?.Value);
    public bool Equals(string? other) => Value.Equals(other);
    public bool Equals(string? other, StringComparison comparison) => Value.Equals(other, comparison);
    public override bool Equals(object? obj)
    {
        return obj switch
        {
            NonEmptyString other => Equals(other),
            string other => Equals(other),
            _ => false
        };
    }

    public override string ToString() => Value;
}
