﻿using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;
using System;

namespace CommonCSSGenerator
{
    public class CSSInputFileEngine
    {

        private string _source = string.Empty;

        private CSSRegEx _regex = null;

        public CSSInputFileEngine(string sourceFolder)
        {
            _source = sourceFolder;

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

                css = css.Replace("\r\n",  string.Empty);

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

            return result;

        }

        private string[] GetLines(string css)
        {

            string format = _regex.RemoveCommentsWithRegEx(css);            

            MatchCollection list = _regex.Groups.Matches(
                format
            ) ;


            var resultList = new List<string>();

            for (int i = 0; i < list.Count; i++)
            {
                resultList.Add(list[i].Value);
            }

            return resultList
                .Where(x => string.IsNullOrEmpty(x) == false)
                .OrderBy(x => x)
                .ToArray();
        }

    }
}