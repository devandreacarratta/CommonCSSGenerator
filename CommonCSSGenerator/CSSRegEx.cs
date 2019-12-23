using System.Text.RegularExpressions;

namespace CommonCSSGenerator
{
    public class CSSRegEx
    {
        private readonly string CSSGroups = @"(?<selector>(?:(?:[^,{]+),?)*?)\{(?:(?<name>[^}:]+):?(?<value>[^};]+);?)*?\}";

        private const string REGEX_REMOVE_CSS_COMMENTS = @"/\*[\d\D]*?\*/";

        private const string REGEX_REMOVE_CSS_SPACES = @"\s+";

        private static Regex _groups = null;

        public Regex Groups
        {
            get
            {
                if (_groups == null)
                {
                    _groups = new Regex(CSSGroups, RegexOptions.Multiline | RegexOptions.IgnoreCase | RegexOptions.Compiled);
                }
                return _groups;
            }
        }

        public string RemoveCommentsWithRegEx(string css)
        {
            string result = Regex.Replace(css, REGEX_REMOVE_CSS_COMMENTS, string.Empty);
            return result;
        }

        public string RemoveSpaceWithRegEx(string css)
        {
            string result = Regex.Replace(css, REGEX_REMOVE_CSS_SPACES, string.Empty);
            return result;
        }
    }
}