using System;
using System.Configuration;
using System.IO;
using System.Linq;

namespace CommonCSSGenerator
{
    class Program
    {
        static void Main(string[] args)
        {

            string sourceFolder = ConfigurationManager.AppSettings["SourceFolder"];
            string outputFolderCSS = ConfigurationManager.AppSettings["OutputFolderCSS"];
            string outputFolderJSON = ConfigurationManager.AppSettings["OutputFolderJSON"];


            CSSInputFileEngine engine = new CSSInputFileEngine(sourceFolder, outputFolderJSON);
            CSSFileDefinition definition = engine.DoWork();

            
            CSSOutputFileEngine feo = new CSSOutputFileEngine(definition,outputFolderCSS,outputFolderJSON)
            {
                TrimStyle = true
            };
            feo.DoWork();

        }
    }
}