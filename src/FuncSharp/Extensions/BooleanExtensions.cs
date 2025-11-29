using System;
using System.Threading.Tasks;

namespace FuncSharp;

public static class BooleanExtensions
{
    /// <summary>
    /// Returns the result of implication. So either the boolean is false or both have to be true.
    /// </summary>
    public static bool Implies(this bool a, bool b)
    {
        return !a || b;
    }

    /// <summary>
    /// Returns the result of implication. So either the boolean is false or both have to be true.
    /// </summary>
    public static bool Implies(this bool a, Func<bool> b)
    {
        return a.Implies(b());
    }
}