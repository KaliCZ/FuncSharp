using FsCheck;
using FsCheck.Xunit;
using FuncSharp.Tests.Generative;
using Xunit;

namespace FuncSharp.Tests.Options;

public class ObjectMapTestsTests
{
    [Fact]
    public void MapTest()
    {
        decimal? value1 = default;
        string? value2 = default;

        string? result1 = value1.Map(d => d.ToString());
        string? result2 = value1.Map(d => "asdf");
        decimal? result3 = value2.Map(d => decimal.Parse(d));
        decimal? result4 = value2.Map(d => (decimal?)null);
    }
}