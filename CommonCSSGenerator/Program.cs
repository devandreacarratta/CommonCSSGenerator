using System.Configuration;

namespace CommonCSSGenerator
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            string sourceFolder = ConfigurationManager.AppSettings["SourceFolder"];
            string outputFolderCSS = ConfigurationManager.AppSettings["OutputFolderCSS"];
            string outputFolderJSON = ConfigurationManager.AppSettings["OutputFolderJSON"];

            CSSInputFileEngine engine = new CSSInputFileEngine(sourceFolder, outputFolderJSON);
            CSSFileDefinition definition = engine.DoWork();

            CSSOutputFileEngine feo = new CSSOutputFileEngine(definition, outputFolderCSS, outputFolderJSON)
            {
                TrimStyle = true
            };
            feo.DoWork();
        }
    }
}