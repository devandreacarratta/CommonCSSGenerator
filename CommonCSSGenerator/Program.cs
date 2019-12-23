using System;
using System.IO;

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
            


            var keys = definition.KeysCounter;

            int aaa = 1;
        }
    }
}
