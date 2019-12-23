using System;
using System.Collections.Generic;
using System.Text;

namespace CommonCSSGenerator
{
    public class Engine
    {

        private string _source = string.Empty;
        private string _output = string.Empty;
        public Engine(string sourceFolder, string outputFlder)
        {
            _source = sourceFolder;
            _output = outputFlder;
        }

        public void DoWork(){

        }

    }
}
