﻿using System;
using System.IO;
using System.Linq;

namespace CommonCSSGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please Insert the Input Folder:");
            string source = @"Z:\CSSTest";
// Console.ReadLine();

            Console.WriteLine("Please Insert the Output Folder:");
            string output = @"Z:\CSSTest\out";
            //Console.ReadLine();

            CSSInputFileEngine engine = new CSSInputFileEngine(source, output);
            CSSFileDefinition definition = engine.DoWork();

            string jsonDefinition = Newtonsoft.Json.JsonConvert.SerializeObject(definition,Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(Path.Combine(output, "schema.json"), jsonDefinition);

            CSSOutputFileEngine feo = new CSSOutputFileEngine(definition)
            {
                TrimStyle = true
            };

            var keyValuePairs = feo.DoWork();

            foreach (var item in keyValuePairs)
            {
                string path = Path.Combine(output, item.Key);
                File.WriteAllLines(path, item.Value.ToArray());
            }

            int aaa = 1;
        }
    }
}
