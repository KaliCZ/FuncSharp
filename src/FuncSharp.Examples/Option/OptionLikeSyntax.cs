using System;
using System.Collections.Generic;
using System.Linq;

namespace FuncSharp.Examples;

public static class OptionLikeSyntaxUsages
{
    private static void ParsingInputData()
    {
        int x1 = 2;
        int? x2 = default;
        string x3 = "";
        string? x4 = default;

        int y1 = x1.Where(x => x > 4); // TODO: verify what happens.
        int? y2 = x2?.Where(x => x > 4);
        string? y3 = x3.Where(x => true);
        string? y4 = x4?.Where(x => true);
    }
}