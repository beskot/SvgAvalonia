using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Svg.Avalonia.Lib
{
    public static class Utility
    {
        public static IEnumerable<string> ExtractTokenContent(string content, params string[] tokens)
        {
            if (string.IsNullOrEmpty(content))
            {
                return default;
            }

            var resultList = new List<string>();
            foreach (var token in tokens)
            {
                if (new Regex($@"({token})\(.+?\)")
                    .Matches(content)
                    .FirstOrDefault() is { } val)
                {
                    resultList.Add(val.Value);
                }
            }

            return resultList;
        }
    }
}