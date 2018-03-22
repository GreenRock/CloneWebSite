using System.Text.RegularExpressions;

namespace Download.Common.Extensions
{
    public class RegexExtension
    {
        public  bool IsMatch(string pattern, string input)
        {
            var regex = new Regex(pattern, RegexOptions.Compiled);
            return regex.IsMatch(input);
        }
        public  Match Match(string pattern, string input)
        {
            var regex = new Regex(pattern, RegexOptions.Compiled);
            return regex.Match(input);
        }
        public MatchCollection Matchs(string pattern, string input)
        {
            var regex = new Regex(pattern, RegexOptions.Compiled);
            return regex.Matches(input);
        }
        public  string Replace(string pattern, string input)
        {
            var regexReplace = new Regex(pattern);
            return regexReplace.Replace(input, "");
        }
    }
}
