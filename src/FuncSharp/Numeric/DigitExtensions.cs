using System.Collections.Generic;
using System.Linq;

namespace FuncSharp;

public static class DigitExtensions
{
    public static Digit? AsDigit(this char value)
    {
        return Digit.TryCreate(value);
    }

    public static IEnumerable<Digit> FilterDigits(this string value)
    {
        return value.Select(Digit.TryCreate).ExceptNulls();
    }
}