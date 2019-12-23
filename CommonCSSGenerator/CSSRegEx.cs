using System;
using System.Text.RegularExpressions;

namespace CommonCSSGenerator
{
    // RegEx from : https://www.codeproject.com/Articles/335850/CSSParser
    class CSSRegEx
    {

        private const string CSSGroups = @"(?<selector>(?:(?:[^,{]+),?)*?)\{(?:(?<name>[^}:]+):?(?<value>[^};]+);?)*?\}";

        private const string CSSComments = @"(?<!"")\/\*.+?\*\/(?!"")";


        private static Regex _groups = null;
        public Regex Groups
        {
            get
            {

                if (_groups == null)
                {
                    _groups = new Regex(CSSGroups, RegexOptions.IgnoreCase | RegexOptions.Compiled);
                }
                return _groups;
            }
        }

        private static Regex _comments = null;
        public Regex Comments
        {
            get
            {

                if (_comments == null)
                {
                    _comments = new Regex(CSSComments, RegexOptions.IgnoreCase | RegexOptions.Compiled);
                }
                return _comments;
            }
        }

    }
}