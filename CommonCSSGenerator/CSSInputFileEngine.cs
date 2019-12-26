using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

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

                    string rowNoSpace = _regex.RemoveSpaceWithRegEx(row);

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

            string format = _regex.RemoveCommentsWithRegEx(css);

            // Keylist
            MatchCollection KeyFramesList = _regex.Keyframes.Matches(
                format
            );

            foreach (var item in KeyFramesList.Select(x=>x.Value))
            {
                resultList.Add(item);
            }

            format = _regex.RemoveKeyframesWithRegEx(format);

            // Media
            MatchCollection mediaList = _regex.Media.Matches(
                format
            );

            foreach (var item in mediaList.Select(x => x.Value))
            {
                resultList.Add(item);
            }

            format = _regex.RemoveMediaWithRegEx(format);
            // Media

            MatchCollection groupsList = _regex.Groups.Matches(
                format
            );

            if (groupsList != null && groupsList.Count > 0)
            {
                for (int i = 0; i < groupsList.Count; i++)
                {
                    resultList.Add(groupsList[i].Value);
                }
            }

            return resultList
                .Where(x => string.IsNullOrEmpty(x) == false)
                .ToArray();
        }
    }
}