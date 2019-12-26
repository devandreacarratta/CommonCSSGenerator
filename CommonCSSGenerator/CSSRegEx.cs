using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace CommonCSSGenerator
{
    public class CSSRegEx
    {
        public readonly string REGEX_CSS_GROUP = @"(?<selector>(?:(?:[^,{]+),?)*?)\{(?:(?<name>[^}:]+):?(?<value>[^};]+);?)*?\}";

        public readonly string REGEX_CSS_KEYFRAMES = @"@(-moz-|-webkit-|-ms-)*keyframes\s(.*?){([0-9%a-zA-Z,\s.]*{(.*?)})*[\s\n]*}";

        public readonly string REGEX_CSS_MEDIA = @"@media\s(.*?){[\s\S]*([\s\S]*[0-9%a-zA-Z,\s.]*{(.*?)})*[\s\n]*}";

        public readonly string REGEX_CSS_COMMENTS = @"/\*[\d\D]*?\*/";

        public readonly string REGEX_CSS_SPACES = @"\s+";

        public void FindAndRemoveTextByRegex(string regexSyntax, ref string css, List<string> result)
        {
            Regex regex = new Regex(regexSyntax, RegexOptions.Multiline | RegexOptions.IgnoreCase | RegexOptions.Compiled);

            MatchCollection matches = regex.Matches(css);

            if (result != null)
            {
                var resultList = matches.Select(x => x.Value);

                result.AddRange(resultList);
            }

            css = Regex.Replace(css, regexSyntax, string.Empty);
        }
    }
}