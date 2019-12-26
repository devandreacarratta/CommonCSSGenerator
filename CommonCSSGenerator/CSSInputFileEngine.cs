using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CommonCSSGenerator
{
    public class CSSInputFileEngine
    {
        private string _source = string.Empty;
        private string _outputFolderJson = string.Empty;

        private CSSRegEx _regex = null;

        public CSSInputFileEngine(string sourceFolder, string outputFolderJson)
        {
            _source = sourceFolder;
            _outputFolderJson = outputFolderJson;

            _regex = new CSSRegEx();
        }

        public CSSFileDefinition DoWork()
        {
            CSSFileDefinition result = new CSSFileDefinition();

            var files = Directory.GetFiles(_source, "*.css");

            foreach (var item in files)
            {
                string css = File.ReadAllText(item);

                if (string.IsNullOrEmpty(css))
                {
                    continue;
                }

                css = css.Replace("\r\n", string.Empty);

                var styles = GetLines(css);

                if (styles == null || styles.Length == 0)
                {
                    continue;
                }

                string fileName = new FileInfo(item).Name;

                for (int idxStyle = 0; idxStyle < styles.Length; idxStyle++)
                {
                    string row = styles[idxStyle];

                    string rowNoSpace = row.Clone() as string;

                    _regex.FindAndRemoveTextByRegex(_regex.REGEX_CSS_SPACES, ref rowNoSpace, null);

                    result.Add(fileName, rowNoSpace, row, idxStyle);
                }
            }

            string jsonPath = Path.Combine(_outputFolderJson, "CSSFileDefinition.json");
            FileHelper.WriteJSON<CSSFileDefinition>(jsonPath, result);

            return result;
        }

        private string[] GetLines(string css)
        {
            var resultList = new List<string>();

            _regex.FindAndRemoveTextByRegex(_regex.REGEX_CSS_COMMENTS, ref css, null);
            _regex.FindAndRemoveTextByRegex(_regex.REGEX_CSS_KEYFRAMES, ref css, resultList);
            _regex.FindAndRemoveTextByRegex(_regex.REGEX_CSS_MEDIA, ref css, resultList);
            _regex.FindAndRemoveTextByRegex(_regex.REGEX_CSS_GROUP, ref css, resultList);

            return resultList
                .Where(x => string.IsNullOrEmpty(x) == false)
                .ToArray();
        }
    }
}