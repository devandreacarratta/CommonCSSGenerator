using System;
using System.Collections.Generic;
using System.Linq;

namespace CommonCSSGenerator
{
    public class CSSOutputFileEngine
    {

        private CSSFileDefinition _definition;

        public bool TrimStyle { get; set; }

        public CSSOutputFileEngine(CSSFileDefinition definition)
        {
            _definition = definition;


            var keyCounter = _definition.KeysCounter(true);
        }

        public SortedDictionary<string, List<string>> DoWork()
        {
            string commonFileId = $"common-{Guid.NewGuid()}.css";

            SortedDictionary<string, List<string>> results = new SortedDictionary<string, List<string>>();
            //results.Add(commonFileId, new List<string>());

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
                        if (results.ContainsKey(itemKeyCommon) == false)
                        {
                            results.Add(itemKeyCommon, new List<string>());
                        }
                        results[itemKeyCommon].Add(style);
                        commonToSkip.Add(css.Key);
                    }
                    else
                    {
                        string itemKey = $"{prefile}{item.Key}";
                        if (results.ContainsKey(itemKey) == false)
                        {
                            results.Add(itemKey, new List<string>());
                        }
                        results[itemKey].Add(style);
                    }

                }

            }

            return results;

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