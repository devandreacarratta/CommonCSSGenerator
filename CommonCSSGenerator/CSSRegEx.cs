using System.Text.RegularExpressions;

namespace CommonCSSGenerator
{
    public class CSSRegEx
    {
        private readonly string CSSGroups = @"(?<selector>(?:(?:[^,{]+),?)*?)\{(?:(?<name>[^}:]+):?(?<value>[^};]+);?)*?\}";

        private readonly string REGEX_CSS_KEYFRAMES = @"@(-moz-|-webkit-|-ms-)*keyframes\s(.*?){([0-9%a-zA-Z,\s.]*{(.*?)})*[\s\n]*}";

        private readonly string REGEX_CSS_MEDIA = @"@media\s(.*?){[\s\S]*([\s\S]*[0-9%a-zA-Z,\s.]*{(.*?)})*[\s\n]*}";

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

        private static Regex _keyframes = null;
        public Regex Keyframes
        {
            get
            {
                if (_keyframes == null)
                {
                    _keyframes = new Regex(REGEX_CSS_KEYFRAMES, RegexOptions.Multiline | RegexOptions.IgnoreCase | RegexOptions.Compiled);
                }
                return _keyframes;
            }
        }

        private static Regex _media = null;
        public Regex Media
        {
            get
            {
                if (_media == null)
                {
                    _media = new Regex(REGEX_CSS_MEDIA, RegexOptions.Multiline | RegexOptions.IgnoreCase | RegexOptions.Compiled);
                }
                return _media;
            }
        }

        public string RemoveCommentsWithRegEx(string css)
        {
            string result = Regex.Replace(css, REGEX_REMOVE_CSS_COMMENTS, string.Empty);
            return result;
        }

        public string RemoveKeyframesWithRegEx(string css)
        {
            string result = Regex.Replace(css, REGEX_CSS_KEYFRAMES, string.Empty);
            return result;
        }

        public string RemoveMediaWithRegEx(string css)
        {
            string result = Regex.Replace(css, REGEX_CSS_MEDIA, string.Empty);
            return result;
        }

        public string RemoveSpaceWithRegEx(string css)
        {
            string result = Regex.Replace(css, REGEX_REMOVE_CSS_SPACES, string.Empty);
            return result;
        }
    }
}