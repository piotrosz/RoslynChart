using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TestMethodNameQuickFix
{
    public static class StringExtensions
    {
        public static string ToUnderscoreCase(this string name)
        {
            var regex = new Regex(@"([A-Z]|[0-9]+)([a-z]{0,})");
            var result = new List<string>();

            for (Match match = regex.Match(name); match.Success; match = match.NextMatch())
            {
                result.Add(match.Groups[1].ToString().ToLower() + match.Groups[2].ToString());
            }

            return string.Join("_", result.ToArray());
        }
    }

}
