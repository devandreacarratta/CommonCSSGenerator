using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CommonCSSGenerator
{
    public class FileHelper
    {


        public static void WriteCSS(string path, string[] lines)
        {
            File.Delete(path);
            File.WriteAllLines(path, lines);
        }

        public static void WriteJSON<T>(string path, T value)
        {
            string jsonDefinition = Newtonsoft.Json.JsonConvert.SerializeObject(value, Newtonsoft.Json.Formatting.Indented);
            File.Delete(path);
            File.WriteAllText(path, jsonDefinition);
        }

    }
}