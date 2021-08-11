using System;
using Svg.Avalonia.Lib;
using Xunit;

namespace Svg.Avalonia.Tests
{
    public class UtilityTest
    {
        [Fact]
        public void ExtractTokenContentTest()
        {
            Assert.Equal(Utility.ExtractTokenContent("values(8 1.2 3.0)", "values"),
                new System.Collections.Generic.List<string> { "values(8 1.2 3.0)" });
        }
    }
}
