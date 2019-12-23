using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CommonCSSGenerator
{
    public class CSSOutputFileEngine
    {
        private CSSFileDefinition _definition;
        private string _outputFolderCSS;
        private string _outputFolderJSON;

        public bool TrimStyle { get; set; }

        public CSSOutputFileEngine(CSSFileDefinition definition, string outputFolderCSS, string outputFolderJSON)
        {
            _definition = definition;
            this._outputFolderCSS = outputFolderCSS;
            this._outputFolderJSON = outputFolderJSON;

            var keyCounter = _definition.KeysCounter(true);
        }

        private SortedDictionary<string, List<string>> _rowsByFile = null;

        private SortedDictionary<string, List<string>> RowsByFile
        {
            get
            {
                if (_rowsByFile == null)
                {
                    string commonFileId = $"common-{Guid.NewGuid()}.css";

                    _rowsByFile = new SortedDictionary<string, List<string>>();

                    int fileNumber = _definition.Items.Count;

                    List<string> commonToSkip = new List<string>();

                    foreach (var item in _definition.Items)
                    {
                        foreach (var css in item.Value)
                        {
                            if (commonToSkip.Contains(css.Key))
                            {
                                continue;
                            }

                            int itemCounter = _definition.KeysCounter().Where(x => x.Key == css.Key).Select(x => x.Value).FirstOrDefault();

                            bool rowAllFiles = (fileNumber == itemCounter);

                            var style = item.Value.Where(x => x.Key == css.Key).Select(x => x.StyleOriginalValue).FirstOrDefault();

                            if (this.TrimStyle)
                            {
                                style = style.Trim();
                            }

                            bool validStyleRow = CheckValidStyleRow(style);

                            string prefile = (validStyleRow ? string.Empty : "ERR-");

                            if (rowAllFiles)
                            {
                                string itemKeyCommon = $"{prefile}{commonFileId}";
                                if (_rowsByFile.ContainsKey(itemKeyCommon) == false)
                                {
                                    _rowsByFile.Add(itemKeyCommon, new List<string>());
                                }
                                _rowsByFile[itemKeyCommon].Add(style);
                                commonToSkip.Add(css.Key);
                            }
                            else
                            {
                                string itemKey = $"{prefile}{item.Key}";
                                if (_rowsByFile.ContainsKey(itemKey) == false)
                                {
                                    _rowsByFile.Add(itemKey, new List<string>());
                                }
                                _rowsByFile[itemKey].Add(style);
                            }
                        }
                    }
                }
                return _rowsByFile;
            }
        }

        public bool DoWork()
        {
            try
            {
                foreach (var item in this.RowsByFile)
                {
                    string cssPath = Path.Combine(_outputFolderCSS, item.Key);
                    string jsonPath = Path.Combine(_outputFolderJSON, item.Key).Replace("css", "json");

                    FileHelper.WriteCSS(cssPath, item.Value.ToArray());
                    FileHelper.WriteJSON<KeyValuePair<string, List<string>>>(jsonPath, item);
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private const char CHAR_OPEN = '{';
        private const char CHAR_CLOSE = '}';
        private readonly char[] CHARS_TO_CHECK = new char[] { CHAR_OPEN, CHAR_CLOSE };

        private bool CheckValidStyleRow(string style)
        {
            var counts = style.GroupBy(c => c)
                            .ToDictionary(grp => grp.Key, grp => grp.Count());

            int open = -1;
            int close = -1;

            if (counts.ContainsKey(CHAR_OPEN))
            {
                open = counts[CHAR_OPEN];
            }

            if (counts.ContainsKey(CHAR_CLOSE))
            {
                close = counts[CHAR_CLOSE];
            }

            bool result = (open > 0 && close > 0 && open == close);

            return result;
        }
    }
}