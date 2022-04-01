using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seek4Treasure.Class
{
    class Options
    {
        [Option("lang", Required = false, Default = "all", HelpText = "--lang csharp")]
        public string lang { get; set; }

        [Option("folder", Required = true, HelpText = @"--folder C:\User\Download")]
        public string folder { get; set; }

        [Option("output", Required = false, Default = "false", HelpText = @"--outputPath C:\User\Desktop")]
        public string output { get; set; }

    }
}
