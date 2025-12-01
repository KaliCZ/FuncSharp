using System;
using System.Collections.Generic;
using System.Linq;

namespace FuncSharp.Examples;

public static class OptionLikeSyntaxUsages
{
    public static void Map(decimal number, decimal divisor)
    {
        decimal? value1 = default;
        string? value2 = default;

        string? result1 = value1.Map(d => d.ToString());
        string? result2 = value1.Map(d => "asdf");
        decimal? result3 = value2.Map(d => decimal.Parse(d));
        decimal? result4 = value2.Map(d => (decimal?)null);
    }

    public static decimal? Divide(decimal number, decimal divisor)
    {
        decimal divisor1 = default;
        decimal? divisor2 = default;
        string divisor3 = "";
        string? divisor4 = default;

        decimal x1 = divisor1.Condition(d => d != 0, d => number / d);
        decimal? x2 = divisor2.ConditionalMap(d => d != 0, d => number / d);
        var x3 = divisor3.ConditionalMap(d => d != 0, d => number / d);
        var x4 = divisor4.ConditionalMap(d => d != 0, d => number / d);

        return divisor.ConditionalMap(d => d != 0, d => number / d);
    }
}